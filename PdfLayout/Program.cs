
PDFLayoutColumn[] columns = new PDFLayoutColumn[3];
columns[0] = new PDFLayoutColumn(3, 7);
columns[1] = new PDFLayoutColumn(3, 7);
columns[2] = new PDFLayoutColumn(3, 7);

PDFLayout layout = new PDFLayout(new PDFLayoutSettings(columns));

char[] alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

int range = 8;
Random r = new Random();
double minimum = 0.5;
double maximum = 8.0;

foreach(char c in alpha)
{
    double random = r.NextDouble() * (maximum - minimum) + minimum;
    layout.AddObjectToColumn(new PDFColumnObject(random, c.ToString()));    
}

layout.PrintLayout();
