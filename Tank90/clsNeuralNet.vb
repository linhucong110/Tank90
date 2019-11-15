''' <summary>
''' 神经网络类,用于创建神经网络对象
''' </summary>
''' <remarks></remarks>
Public Class clsNeuralNet

#Region "私有变量及方法"

    Dim m_NumInputs As Integer  '进入神经细胞的输入数量
    Dim m_NumOutputs As Integer '神经细胞的输出数量
    Dim m_NumHiddenLayers As Integer    '神经网络层的数量(包括输入,输出层)
    Dim m_NeuronsPerHiddenLyr As Integer    '隐藏层中神经细胞数量
    '为每一层(包括输出层)存放所有的神经细胞的存储器
    Dim m_vecLayers As New List(Of SNeuronLayer)    '保存每个神经元层storage for each layer of neurons including the output layer

#End Region

#Region "公共变量及方法"

    ''' <summary>
    ''' 初始化神经网络神经细胞输入数量,输出数量,隐藏层数量,隐藏层神经细胞数量,并创建.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        m_NumInputs = CParams.iNumInputs
        m_NumOutputs = CParams.iNumOutputs
        m_NumHiddenLayers = CParams.iNumHidden
        m_NeuronsPerHiddenLyr = CParams.iNeuronsPerHiddenLayer

        CreateNet()
    End Sub

    ''' <summary>
    ''' 记录所有网络权重的边界点
    ''' </summary>
    ''' <returns>边界点的清单</returns>
    ''' <remarks></remarks>
    Public Function CalculateSplitPoints() As List(Of Integer)
        Dim SplitPoints As New List(Of Integer), i, j, k As Integer
        Dim WeightCounter As Integer = 0

        '对每一层
        For i = 0 To m_NumHiddenLayers
            '对每一个神经细胞
            For j = 0 To m_vecLayers(i).m_NumNeurons - 1
                '对每一个权重
                For k = 0 To m_vecLayers(i).m_vecNeurons(j).m_NumInputs
                    WeightCounter += 1
                Next
                SplitPoints.Add(WeightCounter - 1)
            Next
        Next
        Return SplitPoints
    End Function

    ''' <summary>
    ''' 根据神经网络中神经细胞输入数量,输出数量,隐藏层数量,隐藏层神经细胞数量创建神经网络.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub CreateNet()
        Dim i As Integer

        If (m_NumHiddenLayers > 0) Then
            'create first hidden layer
            '创建输入层神经细胞
            m_vecLayers.Add(New SNeuronLayer(m_NeuronsPerHiddenLyr, m_NumInputs))

            '创建隐藏层神经细胞
            For i = 0 To m_NumHiddenLayers - 2
                m_vecLayers.Add(New SNeuronLayer(m_NeuronsPerHiddenLyr, m_NeuronsPerHiddenLyr))
            Next
            'create output layer
            '创建输出层神经细胞
            m_vecLayers.Add(New SNeuronLayer(m_NumOutputs, m_NeuronsPerHiddenLyr))
        Else
            'create output layer
            '创建输出层神经细胞(只有一层的神经网络)
            m_vecLayers.Add(New SNeuronLayer(m_NumOutputs, m_NumInputs))
        End If
    End Sub

    'returns a vector containing the weights
    ''' <summary>
    ''' 获取整个神经网络的权重值清单
    ''' </summary>
    ''' <returns>整个神经网络的权重清单</returns>
    ''' <remarks></remarks>
    Public Function GetWeights() As List(Of Double)
        Dim weights As New List(Of Double)
        Dim i, j, k As Integer

        For i = 0 To m_NumHiddenLayers
            For j = 0 To m_vecLayers(i).m_NumNeurons - 1
                'for each weight
                For k = 0 To m_vecLayers(i).m_vecNeurons(j).m_NumInputs - 1
                    weights.Add(m_vecLayers(i).m_vecNeurons(j).m_vecWeight(k))
                Next
            Next
        Next
        Return weights
    End Function

    ''' <summary>
    ''' 设置整个神经网络的权重值
    ''' </summary>
    ''' <param name="weights">整个神经网络的权重清单</param>
    ''' <remarks></remarks>
    Public Sub PutWeights(ByVal weights As List(Of Double))
        Dim i, j, k As Integer

        Dim cWeight As Integer

        cWeight = 0

        For i = 0 To m_NumHiddenLayers
            For j = 0 To m_vecLayers(i).m_NumNeurons - 1
                For k = 0 To m_vecLayers(i).m_vecNeurons(j).m_NumInputs - 1
                    m_vecLayers(i).m_vecNeurons(j).m_vecWeight(k) = weights(cWeight)
                    cWeight += 1
                Next
            Next
        Next

    End Sub

    'returns the total number of weights needed for the net
    ''' <summary>
    ''' 获取整个神经网络的权重数量
    ''' </summary>
    ''' <returns>整个神经网络的权重数量</returns>
    ''' <remarks></remarks>
    Public Function GetNumberOfWeights() As Integer
        Dim weights, i, j, k As Integer
        weights = 0

        For i = 0 To m_NumHiddenLayers
            For j = 0 To m_vecLayers(i).m_NumNeurons - 1
                For k = 0 To m_vecLayers(i).m_vecNeurons(j).m_NumInputs - 1
                    weights += 1
                Next
            Next
        Next
        Return weights
    End Function

    'given an input vector this function calculates the output vector
    ''' <summary>
    ''' 根据细胞输入值计算输出响应
    ''' </summary>
    ''' <param name="inputs">输入向量</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Update(ByVal inputs() As Double) As Double()
        Dim outputs() As Double  'stores the resultant outputs from each layer

        Dim cWeight, i, j, k As Integer
        Dim netinput As Double = 0
        Dim NumInputs As Integer

        cWeight = 0
        ReDim outputs(0)

        'first check that we have the correct amount of inputs
        '检查输入值数量是否等于细胞输入数量
        If (inputs.Length <> m_NumInputs) Then
            Return outputs
        End If

        'For each layer....
        '计算神经网络每一层的结果
        For i = 0 To m_NumHiddenLayers
            If i > 0 Then
                'inputs.Clear()
                '清除数组,并把上一层输出结果作为下一层的输入值
                ReDim inputs(outputs.Length - 1)
                Array.Copy(outputs, inputs, outputs.Length)
                'inputs.AddRange(outputs)

            End If

            'outputs.Clear()
            '清除结果数组
            ReDim outputs(m_vecLayers(i).m_NumNeurons - 1)

            cWeight = 0

            '//for each neuron sum the (inputs * corresponding weights).Throw 
            '//the total at our sigmoid function to get the output.
            '计算当前层每个细胞的输出结果
            For j = 0 To m_vecLayers(i).m_NumNeurons - 1
                netinput = 0
                NumInputs = m_vecLayers(i).m_vecNeurons(j).m_NumInputs  '细胞输入数量

                'for each weight
                '计算输入值*权重的总和结果
                For k = 0 To NumInputs - 2
                    netinput += m_vecLayers(i).m_vecNeurons(j).m_vecWeight(k) * inputs(cWeight)
                    cWeight += 1
                Next
                'add in the bias
                '加上偏移值*权重的结果
                netinput += m_vecLayers(i).m_vecNeurons(j).m_vecWeight(NumInputs - 1) * CParams.dBias

                '//we can store the outputs from each layer as we generate them. 
                '//The combined activation is first filtered through the sigmoid 
                '//function
                'outputs.Add(Sigmoid(netinput, CParams.dActivationResponse))
                '保存细胞输入的响应值
                outputs(j) = Sigmoid(netinput, CParams.dActivationResponse)
                cWeight = 0
            Next
        Next
        Return outputs
    End Function

    ''' <summary>
    ''' 计算细胞的S响应曲线值,公式(1/e^(-a/p))
    ''' </summary>
    ''' <param name="netinput">输入激励值a(即所有输入*权重的和)</param>
    ''' <param name="response">Sigmoid的P值.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Sigmoid(ByVal netinput As Double, ByVal response As Double) As Double
        Return (1 / (1 + Math.Exp(-netinput / response)))
    End Function
#End Region

End Class
