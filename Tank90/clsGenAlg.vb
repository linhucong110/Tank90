
'the genetic algorithm class
''' <summary>
''' 遗传算法类
''' </summary>
''' <remarks></remarks>
Public Class clsGenAlg

#Region "私有变量及方法"
    'this holds the entire population of chromosomes
    Private m_vecPop As New List(Of SGenome)    '保存基因的染色体

    Dim m_iPopSize As Integer                   '种群数量
    Dim m_iChromoLength As Integer              '染色体长度
    Dim m_dTotalFitness As Double               '总适应分数
    Dim m_dBestFitness As Double                '种群中适应分数最好的分数
    Dim m_dAverageFitness As Double             '种群平均适应分数

    Dim m_dWorstFitness As Double               '种群最差适应分数
    Dim m_iFittestGenome As Integer             '种群中适应分数最好的分数染色体

    Dim m_dMutationRate As Double               '变异率,试试数字范围0.05到0.3
    Dim m_dCrossoverRate As Double              '杂交率 0.7较合适

    Dim m_cGeneration As Integer                '种群代数

    Dim m_vecSplitPoints As New List(Of Integer)    '记录所有网络权重的边界点

    '//-------------------------------------Crossover()-----------------------
    '//	
    '//  given parents and storage for the offspring this method performs
    '//	crossover according to the GAs crossover rate
    '//-----------------------------------------------------------------------
    ''' <summary>
    ''' 基因杂交
    ''' </summary>
    ''' <param name="mum">母体染色体</param>
    ''' <param name="dad">父体染色体</param>
    ''' <param name="baby1">返回母体杂交后染色体</param>
    ''' <param name="baby2">返回父体杂交后染色体</param>
    ''' <remarks></remarks>
    Private Sub Crossover(ByVal mum As List(Of Double), ByVal dad As List(Of Double), _
                          ByRef baby1 As List(Of Double), ByRef baby2 As List(Of Double))

        'just return parents as offspring dependent on the rate or if parents are the same
        '如果父母染色体一样则返回父母值
        If ((RandFloat() > m_dCrossoverRate) Or CompareArray(mum, dad)) Then
            baby1.AddRange(mum)
            baby2.AddRange(dad)
            Return
        End If

        Dim i, cp As Integer

        'determine a crossover point
        '随机确定一个杂交点位置
        cp = RandInt(0, m_iChromoLength - 1)

        '不杂交部分
        For i = 0 To cp - 1
            baby1.Add(mum(i))
            baby2.Add(dad(i))
        Next

        '杂交部分
        For i = cp To mum.Count - 1
            baby1.Add(dad(i))
            baby2.Add(mum(i))
        Next
    End Sub

    ''' <summary>
    ''' 通过选取基因权重分隔点来杂交,避免基因切断的问题.
    ''' </summary>
    ''' <param name="mum">母体染色体</param>
    ''' <param name="dad">父体染色体</param>
    ''' <param name="baby1">返回母体杂交后染色体</param>
    ''' <param name="baby2">返回父体杂交后染色体</param>
    ''' <remarks></remarks>
    Private Sub CrossoverAtSplits(ByVal mum As List(Of Double), ByVal dad As List(Of Double), _
                          ByRef baby1 As List(Of Double), ByRef baby2 As List(Of Double))
        'just return parents as offspring dependent on the rate or if parents are the same
        '如果超过了杂交率,就不再进行杂交,把两个上代作为两个子代输出
        '如果父母染色体一样则返回父母值
        If ((RandFloat() > m_dCrossoverRate) Or CompareArray(mum, dad)) Then
            baby1.AddRange(mum)
            baby2.AddRange(dad)
            Return
        End If

        Dim i, index1, index2, cp1, cp2 As Integer

        '随机获取两个权重分隔点
        index1 = RandInt(0, m_vecSplitPoints.Count - 2)
        index2 = RandInt(index1, m_vecSplitPoints.Count - 1)

        cp1 = m_vecSplitPoints(index1)
        cp2 = m_vecSplitPoints(index2)

        For i = 0 To mum.Count - 1
            If i < cp1 Or i >= cp2 Then
                '如果在杂交点外则不杂交部分
                baby1.Add(mum(i))
                baby2.Add(dad(i))
            Else
                '对中间段进行杂交部分
                baby1.Add(dad(i))
                baby2.Add(mum(i))
            End If

        Next

    End Sub


    '//	mutates a chromosome by perturbing its weights by an amount not 
    '//	greater than CParams::dMaxPerturbation
    ''' <summary>
    ''' 基因突变,变异干扰参数dMutationRate,在浮点数中一般设置高点,这里设0.1
    ''' </summary>
    ''' <param name="chromo">要突变的染色体(权重权)</param>
    ''' <remarks></remarks>
    Private Sub Mutate(ByRef chromo As List(Of Double))
        Dim i As Integer

        For i = 0 To chromo.Count - 1
            '随机产生一个数,并检查是否小于突变数
            If (RandFloat() < m_dMutationRate) Then 'do we perturb this weight?
                'add or subtract a small value to the weight
                '基因突变. 重新产生一个权重值
                chromo(i) += (RandomClamped() * CParams.dMaxPerturbation)
            End If
        Next
    End Sub

    'returns a chromo based on roulette wheel sampling
    ''' <summary>
    ''' 赌轮选择染色体
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetChromoRoulette() As SGenome
        Dim Slice As Double
        Dim i As Integer
        Dim TheChosenOne As New SGenome(0)
        Dim FitnessSoFar As Double

        'generate a random number between 0 & total fitness count
        '在赌轮上随机产生一个切片
        Slice = (RandFloat() * m_dTotalFitness)

        'go through the chromosones adding up the fitness so far
        FitnessSoFar = 0

        For i = 0 To m_iPopSize - 1
            FitnessSoFar += m_vecPop(i).dFitness

            'if the fitness so far > random number return the chromo at this point
            '如果总适应分数大于随机数则选择此染色体
            If (FitnessSoFar >= Slice) Then
                TheChosenOne = m_vecPop(i)
                Exit For
            End If
        Next

        Return TheChosenOne
    End Function

    '//-------------------------GrabNBest----------------------------------
    '//
    '//	This works like an advanced form of elitism by inserting NumCopies
    '//  copies of the NBest most fittest genomes into a population vector
    '//--------------------------------------------------------------------
    '通过将NBest最适合的基因组的NumCopies副本插入到群体载体中，这就像精英主义的高级形式
    ''' <summary>
    ''' 添加精英基因到基因轮盘中,增加精英基因被选中的概率
    ''' </summary>
    ''' <param name="NBest">要添加的精英数量</param>
    ''' <param name="NumCopies">添加每个精英的Copy数量</param>
    ''' <param name="vecPop">排序后的基因</param>
    ''' <remarks></remarks>
    Private Sub GrabNBest(ByVal NBest As Integer, ByVal NumCopies As Integer, ByRef vecPop As List(Of SGenome))
        Dim i As Integer

        While NBest > 0
            For i = 0 To NumCopies - 1
                vecPop.Add(m_vecPop(m_iPopSize - 1 - NBest))
            Next
            NBest -= 1
        End While
    End Sub

    '//-----------------------------FitnessScaleRank----------------------
    '//
    '//	This type of fitness scaling sorts the population into ascending
    '//	order of fitness and then simply assigns a fitness score based 
    '//	on its position in the ladder. (so if a genome ends up last it
    '//	gets score of zero, if best then it gets a score equal to the size
    '//	of the population. You can also assign a multiplier which will
    '//	increase the 'seperation' of genomes on the ladder and allow the 
    '//	population to converge much quicker
    '//---------------------------------------------------------------------
    Private Sub FitnessScaleRank()
        Dim i, FitnessMultiplier As Integer
        FitnessMultiplier = 1

        Dim tmpG As SGenome

        '//assign fitness according to the genome's position on
        '//this new fitness 'ladder'
        '随机分适应分数
        For i = 0 To m_iPopSize - 1
            tmpG = m_vecPop(i)
            tmpG.dFitness = i * FitnessMultiplier
            m_vecPop(i) = tmpG
        Next
        '//recalculate values used in selection
        CalculateBestWorstAvTot()
    End Sub

    '//-----------------------CalculateBestWorstAvTot-----------------------	
    '//
    '//	calculates the fittest and weakest genome and the average/total 
    '//	fitness scores
    '//---------------------------------------------------------------------
    ''' <summary>
    ''' 计算适应性分数的最好,最差及平均值对应的基因编号
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CalculateBestWorstAvTot()
        Dim HighestSoFar, LowestSoFar As Double
        Dim i As Integer

        m_dTotalFitness = 0
        HighestSoFar = 0
        LowestSoFar = 9999999

        For i = 0 To m_iPopSize - 1
            'update fittest if necessary
            '更新高好的适应分数
            If (m_vecPop(i).dFitness > HighestSoFar) Then
                HighestSoFar = m_vecPop(i).dFitness
                m_iFittestGenome = i
                m_dBestFitness = HighestSoFar
            End If

            'update worst if necessary
            '更新最差适应分数
            If (m_vecPop(i).dFitness < LowestSoFar) Then
                LowestSoFar = m_vecPop(i).dFitness
                m_dWorstFitness = LowestSoFar
            End If

            m_dTotalFitness += m_vecPop(i).dFitness
        Next

        '平均适应分数
        m_dAverageFitness = m_dTotalFitness / m_iPopSize
    End Sub

    Private Sub Reset()
        m_dTotalFitness = 0
        m_dBestFitness = 0
        m_dWorstFitness = 9999999
        m_dAverageFitness = 0
    End Sub

#End Region

#Region "公共变量及方法"

    Public Sub New(ByVal popsize As Integer, ByVal MutRat As Double, ByVal CrossRat As Double,
                   ByVal numweights As Integer, ByVal Splits As List(Of Integer))

        m_iPopSize = (popsize)
        m_dMutationRate = (MutRat)
        m_dCrossoverRate = (CrossRat)
        m_iChromoLength = (numweights)
        m_dTotalFitness = (0)
        m_cGeneration = (0)
        m_iFittestGenome = (0)
        m_dBestFitness = (0)
        m_dWorstFitness = (99999999)
        m_dAverageFitness = (0)
        m_vecSplitPoints = Splits

        Dim i, j As Integer

        For i = 0 To m_iPopSize - 1
            m_vecPop.Add(New SGenome(0))

            For j = 0 To m_iChromoLength - 1
                m_vecPop(i).vecWeights.Add(RandomClamped)
            Next
        Next
    End Sub

    '//-----------------------------------Epoch()-----------------------------
    '//
    '//	takes a population of chromosones and runs the algorithm through one
    '//	 cycle.
    '//	Returns a new population of chromosones.
    '//
    '//-----------------------------------------------------------------------
    ''' <summary>
    ''' 使用遗传算法中的杂交,变异更新基因中的染色体,产生一个新的基因
    ''' </summary>
    ''' <param name="old_Pop">旧的基因</param>
    ''' <returns>新的基因</returns>
    ''' <remarks></remarks>
    Public Function Epoch(ByVal old_Pop() As SGenome) As SGenome()
        'assign the given population to the classes population
        m_vecPop.Clear()
        'm_vecPop.AddRange(old_Pop)
        Dim i As Integer

        For i = 0 To old_Pop.Length - 1
            m_vecPop.Add(old_Pop(i))
        Next

        'reset the appropriate variables
        Reset()

        'sort the population (for scaling and elitism)
        '对基因组进行排序(适应性分数)
        m_vecPop.Sort(AddressOf Compare)

        'calculate best, worst, average and total fitness
        CalculateBestWorstAvTot()

        'create a temporary vector to store new chromosones
        Dim vecNewPop As New List(Of SGenome)

        '//Now to add a little elitism we shall add in some copies of the
        '//fittest genomes. Make sure we add an EVEN number or the roulette
        '//wheel sampling will crash
        '现在增加一点精英主义，我们将添加一些最适合的基因组副本
        '确保我们添加偶数个数目否则轮盘赌采样将崩溃
        If ((CParams.iNumCopiesElite * CParams.iNumElite Mod 2) <> 0) Then
            GrabNBest(CParams.iNumElite, CParams.iNumCopiesElite, vecNewPop)
        End If

        '//now we enter the GA loop
        '//repeat until a new population is generated
        Dim mum, dad As SGenome
        Dim baby1, baby2 As New List(Of Double)

        While (vecNewPop.Count < m_iPopSize)

            mum = GetChromoRoulette()
            dad = GetChromoRoulette()

            baby1.Clear()
            baby2.Clear()

            'Crossover(mum.vecWeights, dad.vecWeights, baby1, baby2)
            CrossoverAtSplits(mum.vecWeights, dad.vecWeights, baby1, baby2)

            '//now we mutate
            Mutate(baby1)
            Mutate(baby2)

            '//now copy into vecNewPop population
            vecNewPop.Add(New SGenome(baby1, 0))
            vecNewPop.Add(New SGenome(baby2, 0))
        End While
        'm_vecPop.Clear()
        'm_vecPop.AddRange(vecNewPop)
        m_vecPop = vecNewPop

        Dim tmpG() As SGenome
        ReDim tmpG(m_vecPop.Count - 1)
        m_vecPop.CopyTo(tmpG)

        Return tmpG

    End Function

    Private Function Compare(ByVal val1 As SGenome, ByVal val2 As SGenome) As Integer
        If val1.dFitness > val2.dFitness Then
            Return 1
        End If
        If val1.dFitness = val2.dFitness Then
            Return 0
        End If
        If val1.dFitness < val2.dFitness Then
            Return -1
        End If
    End Function

    Public ReadOnly Property GetChromos() As SGenome()
        Get
            Dim tmpG() As SGenome
            ReDim tmpG(m_vecPop.Count - 1)
            m_vecPop.CopyTo(tmpG)
            Return tmpG
        End Get
    End Property

    Public ReadOnly Property AverageFitness() As Double
        Get
            Return m_dTotalFitness / m_iPopSize
        End Get
    End Property

    Public ReadOnly Property BestFitness() As Double
        Get
            Return m_dBestFitness
        End Get
    End Property

#End Region

End Class
