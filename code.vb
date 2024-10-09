Option Explicit
Sub CompletarCertificados()
    'En la columna de la izquierda modificar el name de la variable hoja1 que se encuentra en la carpeta Microsoft Excel Objetos
    'var
    Dim datoEtiqueta1 As String
    Dim datoEtiqueta2 As String
    Dim datoEtiquetaN As String
    Dim fila As Long
    Dim objPPT As Object
    Dim objPres As Object
    Dim objSld As Object
    Dim objShp As Object

    Set objPPT = CreateObject("Powerpoint.Application")
    objPPT.Visible = True
    
    'Modificar las rutas de acuerdo a donde está el archivo original
    Set objPres = objPPT.Presentations.Open("path\pp.pptm")
    'Esta ruta genera un nuevo archivo con los certificados completos, se puede modificar para guardar en otro lado.
    objPres.SaveAs "path\ppf.pptx"
      
    'Empezamos contando en la fila 2
    fila = 2
 
    Do While Worksheets("nombreHoja").Cells(fila, 1) <> ""
        'Tomamos los valores de cada columna
        datoEtiqueta1 = Worksheets("nombreHoja").Cells(fila, 1)
        datoEtiqueta2 = Worksheets("nombreHoja").Cells(fila, 2)
        datoEtiquetaN = Worksheets("nombreHoja").Cells(fila, 3)
        'Generamos una copia de la diapositiva original
        Set objSld = objPres.Slides(1).Duplicate
        
        For Each objShp In objSld.Shapes
            If objShp.HasTextFrame Then
                If objShp.TextFrame.HasText Then
                    'Cambiamos las etiquetas correspondientes con el valor de cada string guardado en la variable
                    '<ETIQUETA EN POWER POINT> -> valor de excel guardo en la variable datoEtiqueta1,datoEtiqueta2,etc.
                    objShp.TextFrame.TextRange.Replace "<ETIQUETA1>", datoEtiqueta1
                    objShp.TextFrame.TextRange.Replace "<ETIQUETA2>", datoEtiqueta2
                    objShp.TextFrame.TextRange.Replace "<ETIQUETAN>", datoEtiquetaN
                End If
            End If
        Next
        fila = fila + 1
    Loop

    'borramos el último certificado que se genera de más y está vacio
    objPres.Slides(1).Delete
    objPres.Save
End Sub
