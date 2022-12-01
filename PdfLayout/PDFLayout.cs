public class PDFLayout
{

    private readonly PDFLayoutSettings _settings;

    public List<PDFLayoutColumn[]> PagesWithColumns = null!;

    public PDFLayout(PDFLayoutSettings settings)
    {
        PagesWithColumns = new List<PDFLayoutColumn[]>();
        _settings = settings;
        AddPage();
    }

    private void AddPage()
    {
        PagesWithColumns.Add(_settings.Columns);
    }

    public int AddObjectToColumn(PDFColumnObject ColumnObject, bool isRecursive = false)
    {
        int retVal = -1;
        bool failToAdd = false;

        foreach (PDFLayoutColumn[] layoutColumn in PagesWithColumns)
        {           

            for (int i = 0; i < layoutColumn.Length; i++)
            {
                if (layoutColumn[i].AddObjectToColumn(ColumnObject) > -1)
                {
                    Console.WriteLine($"Adding {ColumnObject.Label}");
                    retVal = i;
                    break;
                }
                else                
                    failToAdd = true;                    
            }

            if(retVal > -1)
                break;

            if (failToAdd && !isRecursive)
                break;
        }

        if (failToAdd && !isRecursive)
        {
            AddPage();
            AddObjectToColumn(ColumnObject, true);
        }
        else if (failToAdd && isRecursive)
            Console.WriteLine($"Cant fit {ColumnObject.Label} anywhere"); 

        return retVal;
    }

    public void PrintLayout()
    {
        int page = 1;
        foreach (PDFLayoutColumn[] layoutColumn in PagesWithColumns)
        {
            Console.WriteLine($"Page {page}");

            for (int i = 0; i < layoutColumn.Length; i++)
            {
                Console.WriteLine($"Column {i} has {layoutColumn[i].ColumnObjects.Count} objects - {layoutColumn[i].GetContentsCSV()}");
            }

            page++;
        }
    }

}

public class PDFLayoutSettings
{
    private readonly int _numberOfColumns;
    private readonly PDFLayoutColumn[] _columns;

    public PDFLayoutColumn[] Columns { get { return _columns; } }
    public int NumberOfColumns { get { return _numberOfColumns; } }

    public PDFLayoutSettings(PDFLayoutColumn[] columns)
    {
        if (columns == null || columns.Length == 0)
            throw new ArgumentNullException(nameof(columns));



        _columns = columns;
        _numberOfColumns = _columns.Length;

    }

}

public class PDFLayoutColumn
{
    private readonly double _width;
    private readonly double _height;
    private double availableHeight;
    public double AvailableHeight { get { return availableHeight; } }
    public List<PDFColumnObject> ColumnObjects { get; set; } = new List<PDFColumnObject>();

    public PDFLayoutColumn(double width, double height)
    {
        _width = width;
        _height = height;
        availableHeight = _height;
        ColumnObjects = new List<PDFColumnObject>();
    }

    public int AddObjectToColumn(PDFColumnObject ColumnObject)
    {
        int retVal = -1;

        if (ColumnObject.Height <= 0)
            throw new ArgumentOutOfRangeException(nameof(ColumnObject));

        if (availableHeight < ColumnObject.Height)
            return retVal;

        if (availableHeight >= ColumnObject.Height)
        {
            try
            {
                availableHeight = availableHeight - ColumnObject.Height;
                ColumnObjects.Add(ColumnObject);
                retVal = ColumnObjects.Count;
            }
            catch (Exception ex)
            {
                LogError(ex.Message);
                retVal = -1;
            }
        }

        return retVal;
    }

    private void LogError(string error)
    {
        Console.WriteLine(error);
    }

    public string GetContentsCSV()
    {
        return string.Join(",", ColumnObjects);
    }

}

public class PDFColumnObject
{
    public string Label { get; set; } = string.Empty;
    public double Height { get; set; }

    public PDFColumnObject(double height, string label)
    {
        Height = height;
        Label = label;
    }

    public override string ToString()
    {
        return Label;
    }
}