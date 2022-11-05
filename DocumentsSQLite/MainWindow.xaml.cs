using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DocumentsSQLite
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            UpdateDocumentGrid();
            UpdateDocumentItemsGrid();
        }

        private string Between(string AllString, string FirstString, string LastString)
        {
            string FinalString;
            int Pos1 = AllString.IndexOf(FirstString) + FirstString.Length;
            int Pos2 = AllString.IndexOf(LastString);
            FinalString = AllString.Substring(Pos1, Pos2 - Pos1);
            return FinalString;
        }

        private void ButtonImportClick(object sender, RoutedEventArgs e)
        {
            DatabaseSQLiteSerializer db = new DatabaseSQLiteSerializer();
            if (DatabaseSQLiteSerializer.numbersOfRecords > 1) MessageBox.Show("Plik został już zaimportowany!");
            else db.ReadDocumentsFromCSV();
            UpdateDocumentGrid();
            UpdateDocumentItemsGrid();
        }

        public void UpdateDocumentGrid()
        {
            DatabaseSQLiteSerializer db = new DatabaseSQLiteSerializer();
            var docs = from d in db.Documents
                       select new
                       {
                           ID = d.Document_ID,
                           Type = d.Type,
                           Date = d.Date,
                           FirstName = d.FirstName,
                           LastName = d.LastName,
                           City = d.City
                       };
            this.DocumentGrid.ItemsSource = docs.ToList();
        }

        public void UpdateDocumentItemsGrid()
        {
            DatabaseSQLiteSerializer db = new DatabaseSQLiteSerializer();
            var docs = from doc in db.DocumentItems
                       select new
                       {
                           ID = doc.DocumentItems_ID,
                           Dokument_ID = doc.Document_ID,
                           Ordinal = doc.Ordinal,
                           Product = doc.Product,
                           Quantity = doc.Quantity,
                           Price = doc.Price,
                           TaxRate = doc.TextRate
                       };
            this.DocumentItemsGrid.ItemsSource = docs.ToList();
        }

        private void DocumentGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.DocumentGrid.SelectedItems.Count >= 0 && this.DocumentGrid.SelectedIndex >= 0)
            {
                int selectedDokumentID;
                var row = this.DocumentGrid.SelectedItems[0]?.ToString() ?? "";
                const string idPrefix = "ID = ";
                const string idSufix = ",";
                var Idstring = Between(row, idPrefix, idSufix);
                selectedDokumentID = int.Parse(Idstring);
                DatabaseSQLiteSerializer db = new DatabaseSQLiteSerializer();
                var docs = from doc in db.DocumentItems
                            where doc.Document_ID == selectedDokumentID
                            select new
                            {
                                ID = doc.DocumentItems_ID,
                                Dokument_ID = doc.Document_ID,
                                Ordinal = doc.Ordinal,
                                Product = doc.Product,
                                Quantity = doc.Quantity,
                                Price = doc.Price,
                                TaxRate = doc.TextRate
                            };
                this.SelectedDocumentItemsGrid.ItemsSource = docs.ToList();
            }
        }
    }
}
