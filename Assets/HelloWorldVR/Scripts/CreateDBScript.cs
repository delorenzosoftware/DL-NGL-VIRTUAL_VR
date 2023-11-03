using UnityEngine;
using UnityEngine.UI;
using System.Data;
using Mono.Data.Sqlite;
using System.Collections;
using System;
using System.IO;
using SimpleFileBrowser;

public class CreateDBScript : MonoBehaviour {

	public Text DebugText;

    public event Action<UserExperiment> OnUserExperimentRetrieved;
    public const int EXEPERIMENT_1 = 1;
    public const int EXEPERIMENT_2 = 2;
    public const int EXEPERIMENT_3 = 3;


    // Use this for initialization
    public void Start () {
        FileBrowser.HideDialog();
        
    }

    public IEnumerator CreateTableUserPerformances()
    {

        string conn = SetDataBaseClass.SetDataBase("DataBase.db");


        IDbConnection dbcon;
        IDbCommand dbcmd;
        IDataReader reader;

        dbcon = new SqliteConnection(conn);
        dbcon.Open();

        dbcmd = dbcon.CreateCommand();
        string SqlQuery = "CREATE TABLE if not exists user_performances ( " + 
            "texperiment_no INTEGER, "+"ttimestamp INTEGER," + 
            "texperiment_name TEXT," +
            "tuser TEXT," +
            "tar_vr_identifier INTEGER," +
            "texperiment_done INTEGER)";
        dbcmd.CommandText = SqlQuery;
        reader = dbcmd.ExecuteReader();

        reader.Close();
        dbcon.Close();
        yield return null;
    }

    private IEnumerator InsertUserPerformance(UserExperiment userExperiment, bool experiment_done)
    {
        string conn = SetDataBaseClass.SetDataBase("DataBase.db");

        IDbConnection dbcon;
        IDbCommand dbcmd;

        dbcon = new SqliteConnection(conn);
        dbcon.Open();

        dbcmd = dbcon.CreateCommand();

        string ar_vr_identifier = "1";

        DateTime dataHoraAtual = DateTime.Now;

        long timestamp = dataHoraAtual.Ticks;

        Debug.Log("insert into user_performances");

        string SqlQuery = "insert into user_performances " +
            "(experiment_no , timestamp, experiment_name, user, ar_vr_identifier, experiment_done) values "+
            "(\"" + userExperiment.experimentNo + "\", \"" + timestamp + "\", \""+ userExperiment.experimentName + "\", \"" + userExperiment.userId + "\", \""+ ar_vr_identifier + "\", " + experiment_done + "); ";
        dbcmd.CommandText = SqlQuery;
        dbcmd.ExecuteReader();

        dbcon.Close();

        yield return ExportDatabase();

        
    }

    IEnumerator ExportDatabase()
    {
        Debug.Log("Entrou no ExportDataBase");
        //string databasePath = Path.Combine(Application.persistentDataPath, "DataBase.db"); // Caminho do banco de dados SQLite
        //string exportPath = Application.persistentDataPath; // Pasta de exportação padrão

        // Configure o callback do SimpleFileBrowser
        FileBrowser.SetFilters(true, new FileBrowser.Filter("DataBase", ".db"));
        FileBrowser.SetDefaultFilter(".db");
        FileBrowser.SetExcludedExtensions(".lnk", ".tmp", ".zip", ".rar", ".exe");

        FileBrowser.ShowSaveDialog(successDialog, null, FileBrowser.PickMode.Files, false, null, "DataBase.db", "Create a DataBase.db file", "Save");

        yield return null;
    }

    private void successDialog(string[] path)
    {
        if (!string.IsNullOrEmpty(path[0]))
        {
            try
            {
                Debug.Log("caminhos - " + path[0]);

                string databasePath = Path.Combine(Application.persistentDataPath, "DataBase.db");
                FileBrowserHelpers.CopyFile(databasePath, path[0]);
                Debug.Log("Banco de dados exportado com sucesso para: " + path);
                
            }
            catch (Exception e)
            {
                Debug.LogError("Erro ao exportar o banco de dados: " + e.Message);
            }
        }
    }

    public void completedExeperiment(int currentExperimentNo)
    {

        Coroutine coroutine = StartCoroutine(getUserPerformance(currentExperimentNo));
        //INSERT INTO user_experiments(experiment_no, experiment_name, user) VALUES(1, "VR_EXPERIMENT_01", "Joao")
    }
    
    private IEnumerator getUserExperiment(int currenciExperimetNo)
    {
        string conn = SetDataBaseClass.SetDataBase("DataBase.db");


        IDbConnection dbcon;
        IDbCommand dbcmd;
        IDataReader reader;

        dbcon = new SqliteConnection(conn);
        dbcon.Open();

        dbcmd = dbcon.CreateCommand();

        string SqlQuery = "select experiment_no, experiment_name, user from user_experiments limit 1";
        dbcmd.CommandText = SqlQuery;
        reader = dbcmd.ExecuteReader();

        UserExperiment userExperiment = new UserExperiment(0, "", "");

        Debug.Log("select experiment_no, experiment_name, user from user_experiments");
        int experiment_no = -1;
        while (reader.Read())
        {
            experiment_no = reader.GetInt16(0);
            string experiment_name = reader.GetString(1);
            string userId = reader.GetString(2);
            userExperiment = new UserExperiment(experiment_no, experiment_name, userId);
        }
        reader.Close();
        if (currenciExperimetNo == experiment_no)
        {
            yield return InsertUserPerformance(userExperiment, true); 
        }
        else
        {
            yield return null;
        }
        
    }

    private IEnumerator getUserPerformance(int userExperimentNo)
    {
        string conn = SetDataBaseClass.SetDataBase("DataBase.db");


        IDbConnection dbcon;
        IDbCommand dbcmd;
        IDataReader reader;

        dbcon = new SqliteConnection(conn);
        dbcon.Open();

        dbcmd = dbcon.CreateCommand();

        string SqlQuery = "select user, timestamp, experiment_done from user_performances";
        dbcmd.CommandText = SqlQuery;
        reader = dbcmd.ExecuteReader();

        
        Debug.Log("select user, timestamp, experiment_done from user_performances");
        bool isInserted = false;
        while (reader.Read())
        {
            isInserted = true;
            Debug.Log(reader.GetValue(0) + " - " + reader.GetValue(1) + " - " + reader.GetValue(2));
        }
        reader.Close();
        if (isInserted)
        {
            yield return null;
        }
        else
        {
            yield return getUserExperiment(userExperimentNo);
        }
        
    }
}