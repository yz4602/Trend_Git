using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Data;
using ExcelDataReader;

public class ExcelCommentReader : MonoBehaviour
{
	[HideInInspector]
	public List<string> excelCommentList;
	
	private void Start()
	{
		string filePath = Path.Combine(Application.streamingAssetsPath, "TrendsReactions.xlsx");
		ReadExcelFile(filePath); 
	}

	private void ReadExcelFile(string filePath)
	{
		Debug.Log("Comment test");
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
				excelCommentList.Add(stringValue);
			}
		}

		// Free resources (IExcelDataReader is IDisposable)
		excelReader.Close();
	}
}
