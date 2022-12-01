
PDFLayoutColumn[] columns = new PDFLayoutColumn[3];
columns[0] = new PDFLayoutColumn(3, 7);
columns[1] = new PDFLayoutColumn(3, 4);
columns[2] = new PDFLayoutColumn(3, 7);

PDFLayout layout = new PDFLayout(new PDFLayoutSettings(columns));

char[] alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

int range = 12;
Random r = new Random();

foreach(char c in alpha)
{
    layout.AddObjectToColumn(new PDFColumnObject(r.NextDouble()* range, c.ToString()));    
}

layout.AddObjectToColumn(new PDFColumnObject(2.1, "A"));
layout.AddObjectToColumn(new PDFColumnObject(3.1, "B"));
layout.AddObjectToColumn(new PDFColumnObject(4.8, "C"));
layout.AddObjectToColumn(new PDFColumnObject(7.0, "D"));
layout.AddObjectToColumn(new PDFColumnObject(1.32, "E"));
layout.AddObjectToColumn(new PDFColumnObject(1.2, "F"));

layout.PrintLayout();
