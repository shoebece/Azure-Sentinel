﻿using Microsoft.Azure.Sentinel.KustoServices.Contract;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Kqlvalidations.Tests
{
    public class CustomTablesSchemasLoader : ITableSchemasLoader
    {
        private readonly List<TableSchema> _tableSchemas;
        private const int TestFolderDepth = 3;

        public CustomTablesSchemasLoader()
        {
            _tableSchemas = new List<TableSchema>();
            
            var jsonFilePath = Path.Combine(Utils.GetTestDirectory(TestFolderDepth), "CustomTables");
            var jsonFiles = Directory.GetFiles(jsonFilePath, "*.json");

            foreach (var jsonFile in jsonFiles)
            {
                var tableSchema = ReadTableSchema(jsonFile);
                if (tableSchema != null)
                {
                    _tableSchemas.Add(tableSchema);
                }
            }
        }

        public IEnumerable<TableSchema> Load()
        {
            return _tableSchemas;
        }

        private TableSchema ReadTableSchema(string jsonFile)
        {
            using (StreamReader r = new StreamReader(jsonFile))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<TableSchema>(json);
            }
        }
    }
}
