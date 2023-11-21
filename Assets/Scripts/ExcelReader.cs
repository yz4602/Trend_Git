using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Data;
using ExcelDataReader;

public class ExcelReader : MonoBehaviour
{
	public List<string> excelContentList;
	
	private void Start()
	{
		string filePath = Path.Combine(Application.streamingAssetsPath, "test.xlsx");
		ReadExcelFile(filePath); 
	}

	private void ReadExcelFile(string filePath)
	{
		FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);

		// Auto-detect format, supports:
		IExcelDataReader excelReader = ExcelReaderFactory.CreateReader(stream);

		DataSet result = excelReader.AsDataSet(new ExcelDataSetConfiguration()
		{
			// Gets or sets a value indicating whether to set the DataColumn.DataType 
			// property in a second pass.
			UseColumnDataType = true,

			// Gets or sets a callback to obtain configuration options for a DataTable. 
			ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
			{
				// Gets or sets a value indicating whether to use a row from the 
				// data as column names.
				UseHeaderRow = false
			}
		});

		// The result of each spreadsheet is in result.Tables
		DataTable table = result.Tables[0];
		foreach (DataRow row in table.Rows)
		{
			foreach (var value in row.ItemArray)
			{
				string stringValue = value.ToString();
				if(stringValue == "") continue;
				//Debug.Log(value);
				excelContentList.Add(stringValue);
			}
		}

		// Free resources (IExcelDataReader is IDisposable)
		excelReader.Close();
	}
}
