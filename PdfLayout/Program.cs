
PDFLayoutColumn[] columns = new PDFLayoutColumn[3];
columns[0] = new PDFLayoutColumn(3, 7);
columns[1] = new PDFLayoutColumn(3, 4);
columns[2] = new PDFLayoutColumn(3, 7);

PDFLayout layout = new PDFLayout(new PDFLayoutSettings(columns));

char[] alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

int range = 8;
Random r = new Random();

foreach(char c in alpha)
{
    layout.AddObjectToColumn(new PDFColumnObject(r.NextDouble()* range, c.ToString()));    
}


layout.PrintLayout();
