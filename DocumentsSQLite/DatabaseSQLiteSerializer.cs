using System;
using System.Collections.Generic;
using System.IO;

namespace DocumentsSQLite
{
    public class DatabaseSQLiteSerializer : DatabaseSQLiteContext
    {
        public static int numbersOfRecords = 0;
        public void SaveDocuments(Document documents)
        {
            this.Documents.Add(documents);
            this.SaveChanges();
        }

        public void SaveDocumentItems(DocumentItem documentItem)
        {
            this.DocumentItems.Add(documentItem);
            this.SaveChanges();
        }

        public List<DocumentItem>[] ReadDocumentItemsFromCSV()
        {
            var memory = 0;
            var LineNumber = 0;
            var separator = ';';
            var BufforList = new List<DocumentItem>[2000];
            using (StreamReader reader = new StreamReader(@"DocumentItems.csv"))
            {
                while (!reader.EndOfStream)
                {
                    if (LineNumber != 0)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(separator);

                        if (memory == Int32.Parse(values[0]))
                        {
                            //Nothing
                        }
                        else
                        {
                            memory = Int32.Parse(values[0]);
                            var DocumentItemsList = new List<DocumentItem>();
                            BufforList[memory] = DocumentItemsList;
                        }
                        BufforList[memory].Add(new DocumentItem() {
                        Document_ID = Int32.Parse(values[0]),
                        Ordinal = Int32.Parse(values[1]),
                        Product = values[2],
                        Quantity = Int32.Parse(values[3]),
                        Price = float.Parse(values[4]),
                        TextRate = Int32.Parse(values[5])
                        });
                    }
                    else
                    {
                        var firstLine = reader.ReadLine();
                    }
                    LineNumber++;
                }
            }
            return BufforList;
        }

        public void ReadDocumentsFromCSV()
        {

            var DocumentItems = ReadDocumentItemsFromCSV();
            var LineNumber = 0;
            var separator = ';';
            var DocumentsList = new List<Document>();
            using (StreamReader reader = new StreamReader(@"Documents.csv"))
            {
                while (!reader.EndOfStream)
                {
                    numbersOfRecords++;
                    if (LineNumber != 0)
                    {
                        var line = reader.ReadLine();
                        if(LineNumber == 1)
                        {
                            separator = line!.Contains(',') ? ',' : ';';
                        }                          
                        var values = line!.Split(separator);
                        try
                        {
                            this.SaveDocuments(new Document()
                            {
                                Document_ID = Int32.Parse(values[0]),
                                Type = values[1],
                                Date = DateTime.Parse(values[2]),
                                FirstName = values[3],
                                LastName = values[4],
                                City = values[5],
                                DocumentItems = DocumentItems[Int32.Parse(values[0])]
                            });
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }           
                    else
                    {
                        var firstLine = reader.ReadLine();
                    }
                    LineNumber++;
                }
            }
        }
    }
}
