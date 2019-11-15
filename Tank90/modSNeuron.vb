
Public Module modSNeuron
    ''' <summary>
    ''' 人工神经细胞结构
    ''' </summary>
    ''' <remarks></remarks>
    Public Structure SNeuron    '人工神经细胞结构
        ''' <summary>
        ''' 神经细胞输入端数量
        ''' </summary>
        ''' <remarks></remarks>
        Dim m_NumInputs As Integer  '输入端数量

        ''' <summary>
        ''' 神经细胞每个输入端的权重List
        ''' </summary>
        ''' <remarks></remarks>
        Dim m_vecWeight As List(Of Double) '每个输入端的权重

        ''' <summary>
        ''' 初始化人工神经细胞,并每个输入端的赋权重(-1,1)
        ''' </summary>
        ''' <param name="NumInputs">输入端数量</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal NumInputs As Integer)
            Dim i As Integer

            '要为偏移值也添加输入权重,所以输入数量要+1
            m_NumInputs = (NumInputs + 1)   'we need an additional weight for the bias hence the +1
            m_vecWeight = New List(Of Double)

            '将权重赋一个随机值(-1,1)
            For i = 0 To NumInputs
                m_vecWeight.Add(RandomClamped())    'set up the weights with an initial random value
            Next
        End Sub
    End Structure

    'struct to hold a layer of neurons.
    ''' <summary>
    ''' 神经细胞层结构
    ''' </summary>
    ''' <remarks></remarks>
    Public Structure SNeuronLayer   '神经细胞层结构
        ''' <summary>
        ''' 神经细胞层使用的神经细胞数目
        ''' </summary>
        ''' <remarks></remarks>
        Dim m_NumNeurons As Integer '本层使用的神经细胞数目the number of neurons in this layer

        ''' <summary>
        ''' 神经细胞层的神经细胞List
        ''' </summary>
        ''' <remarks></remarks>
        Dim m_vecNeurons As List(Of SNeuron)    '本层的神经细胞

        ''' <summary>
        ''' 初始化神经细胞层
        ''' </summary>
        ''' <param name="NumNeurons">本层使用的神经细胞数目</param>
        ''' <param name="NumInputsPerNeuron">每个神经细胞输入端数量</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal NumNeurons As Integer, ByVal NumInputsPerNeuron As Integer)
            m_NumNeurons = (NumNeurons)

            Dim i As Integer
            m_vecNeurons = New List(Of SNeuron)

            For i = 0 To NumNeurons - 1
                m_vecNeurons.Add(New SNeuron(NumInputsPerNeuron))
            Next
        End Sub

    End Structure

    'create a structure to hold each genome
    '创建基因结构
    ''' <summary>
    ''' 神经细胞基因结构(权重权)
    ''' </summary>
    ''' <remarks></remarks>
    Public Structure SGenome
        Dim vecWeights As List(Of Double)   '每个神经细胞的权重值
        Dim dFitness As Double  '适应性值

        ''' <summary>
        ''' 初始化基因结构
        ''' </summary>
        ''' <param name="d">初始适应性值</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal d As Double)
            dFitness = d
            vecWeights = New List(Of Double)
        End Sub

        ''' <summary>
        ''' 初始化基因结构
        ''' </summary>
        ''' <param name="w">每个神经细胞的权重值</param>
        ''' <param name="f">初始适应性值</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal w As List(Of Double), ByVal f As Double)
            vecWeights = New List(Of Double)
            vecWeights.AddRange(w)
            dFitness = f
        End Sub

        ''' <summary>
        ''' 重载小于操作符,用于比较基因的适应值大小
        ''' </summary>
        ''' <param name="lhs">操作符左边基因</param>
        ''' <param name="rhs">操作符右边基因</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Shared Operator <(ByVal lhs As SGenome, ByVal rhs As SGenome) As Boolean
            Return lhs.dFitness < rhs.dFitness
        End Operator

        ''' <summary>
        ''' 重载大于操作符,用于比较基因的适应值大小
        ''' </summary>
        ''' <param name="lhs">操作符左边基因</param>
        ''' <param name="rhs">操作符右边基因</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Shared Operator >(ByVal lhs As SGenome, ByVal rhs As SGenome) As Boolean
            Return lhs.dFitness > rhs.dFitness
        End Operator

    End Structure
End Module
