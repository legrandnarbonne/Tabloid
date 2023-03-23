/*
 * Created by SharpDevelop.
 * User: DAPOJERO
 * Date: 19/03/2009
 * Time: 11:11
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace mySqlLib
{
    /// <summary>
    /// Description of mySqlTools.
    /// </summary>
    public static class mySqlTools
    {

        public static string lastError;

        public static string formatDate(DateTime dt)
        {
            return String.Format("{0:yyyy/MM/dd HH:mm:ss}", dt);
        }
        /// <summary>
        /// retourne une table correstondant au select fourni
        /// </summary>
        /// <param name="sql">requete à executer</param>
        /// <param name="connectionString">Chaine de connection</param>
        public static DataTable data(string sql, string connectionString)
        {
            MySqlConnection conn = setConnection(connectionString);
            if (conn!=null) return data(sql,conn);
            return null;
        }
        /// <summary>
        /// retourne une table correstondant au select fourni
        /// </summary>
        /// <param name="sql">requete à executer</param>
        /// <param name="provider">Connecteur MySql</param>
        public static DataTable data(string sql,  MySqlConnection provider)
        {
            DataTable dt = new DataTable();
            try
            {
                
                provider.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(sql, provider);
               adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                lastError = ex.Message + "\n" + sql;
                return null;
            }
            finally
            {
                if (provider.State == System.Data.ConnectionState.Open) provider.Close();
            }
            return dt;
        }
        /// <summary>
        /// affecte une table dans un dataset
        /// </summary>
        /// <param name="sql">requete à executer</param>
        /// <param name="table">nom de la table</param>
        /// <param name="connectionString">chaine de connection</param>
        /// <param name="dset">dataset devant contenir la table</param>
        public static void data(string sql, string table, string connectionString, DataSet dset)
        {
            MySqlConnection conn = setConnection(connectionString);
            if (conn != null)
            {
                data(sql,table,conn,dset);
            }
        }
        /// <summary>
        /// affecte une table dans un dataset
        /// </summary>
        /// <param name="sql">requete à executer</param>
        /// <param name="table">nom de la table</param>
        /// <param name="provider">Connecteur MySql</param>
        /// <param name="dset">dataset devant contenir la table</param>
        public static void data(string sql, string table, MySqlConnection provider, DataSet dset)
        {//MessageBox.Show(sql);
            try
            {
                provider.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(sql, provider);
                if (dset.Tables[table] != null) dset.Tables[table].Clear();
                adapter.Fill(dset, table);
            }
            catch (Exception ex)
            {
                lastError = ex.Message + "\n" + sql;
            }
            finally
            {
                if (provider.State == System.Data.ConnectionState.Open) provider.Close();
            }
            //MessageBox.Show(dset.Tables[table].Rows.Count.ToString());
        }
        /// <summary>
        /// Exécute la commande sql sur le connecteur spécifié
        /// retourne le nombre de ligne modifié ou -1 en cas d'erreur
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="connecteur"></param>
        /// <returns></returns>
        public static int Command(string sql, string connectionString)
        {
            MySqlConnection conn = setConnection(connectionString);
            if (conn != null)
            {
                return Command(sql, conn);
            }
            return -1;
        }
 
        /// <summary>
        /// Exécute la commande sql sur le connecteur spécifié
        /// retourne le nombre de ligne modifié ou -1 en cas d'erreur
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="connecteur"></param>
        /// <returns></returns>
        public static int Command(string sql, MySqlConnection connecteur)
        {
            int rowAffected = -1;
            try
            {
                if (connecteur.State != ConnectionState.Open) connecteur.Open();
                MySqlCommand command = new MySqlCommand(sql, connecteur);
                rowAffected = command.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                lastError = e.ToString() + "\n" + sql;
            }
            finally
            {
                if (connecteur.State == ConnectionState.Open) connecteur.Close();
            }
            return rowAffected;
        }
        public static string filtre(string src)
        {
            src = src.Replace("\"", "''");
            return src.Replace("'", "''");
        }
        public static MySqlConnection setConnection(string connectionString)
        {
            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection(connectionString);//
            }
            catch (Exception ex)
            {
                lastError = ex.ToString() + "\n" + connectionString;
            }
            return conn;
        }
    }
}
