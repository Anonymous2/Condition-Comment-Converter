using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Condition_Comment_Converter
{
    class WorldDatabase : Database<MySqlConnection, MySqlConnectionStringBuilder, MySqlParameter, MySqlCommand, MySqlTransaction>
    {
        public WorldDatabase(string host, uint port, string username, string password, string databaseName)
        {
            connectionString = new MySqlConnectionStringBuilder();
            connectionString.Server = host;
            connectionString.Port = (uint)port;
            connectionString.UserID = username;
            connectionString.Password = password;
            connectionString.Database = databaseName;
            connectionString.AllowUserVariables = true;
            connectionString.AllowZeroDateTime = true;
        }

        public async Task<int> GetCreatureIdByGuid(int guid)
        {
            DataTable dt = await ExecuteQuery("SELECT id FROM creature WHERE guid = '" + guid + "'");

            if (dt.Rows.Count == 0)
                return 0;

            return Convert.ToInt32(dt.Rows[0]["id"]);
        }

        public async Task<int> GetGameobjectIdByGuid(int guid)
        {
            DataTable dt = await ExecuteQuery("SELECT id FROM gameobject WHERE guid = '" + guid + "'");

            if (dt.Rows.Count == 0)
                return 0;

            return Convert.ToInt32(dt.Rows[0]["id"]);
        }

        public async Task<string> GetCreatureNameById(int id)
        {
            DataTable dt = await ExecuteQuery("SELECT name FROM creature_template WHERE entry = '" + id + "'");

            if (dt.Rows.Count == 0)
                return "_replaceBecauseOfError_ GetCreatureNameById not found";

            string name = dt.Rows[0]["name"].ToString();
            return name.Replace('"', '\'');
        }

        public async Task<string> GetCreatureNameByGuid(int guid)
        {
            DataTable dt = await ExecuteQuery("SELECT name FROM creature_template WHERE entry = '" + await GetCreatureIdByGuid(guid) + "'");

            if (dt.Rows.Count == 0)
                return "_replaceBecauseOfError_ GetCreatureNameByGuid not found";

            string name = dt.Rows[0]["name"].ToString();
            return name.Replace('"', '\'');
        }

        public async Task<string> GetGameobjectNameById(int id)
        {
            DataTable dt = await ExecuteQuery("SELECT name FROM gameobject_template WHERE entry = '" + id + "'");

            if (dt.Rows.Count == 0)
                return "_replaceBecauseOfError_ GetGameobjectNameById not found";

            string name = dt.Rows[0]["name"].ToString();
            return name.Replace('"', '\'');
        }

        public async Task<string> GetGameobjectNameByGuid(int guid)
        {
            DataTable dt = await ExecuteQuery("SELECT name FROM gameobject_template WHERE entry = '" + await GetGameobjectIdByGuid(guid) + "'");

            if (dt.Rows.Count == 0)
                return "_replaceBecauseOfError_ GetGameobjectNameByGuid not found";

            string name = dt.Rows[0]["name"].ToString();
            return name.Replace('"', '\'');
        }

        public async Task<string> GetSpellNameById(int id)
        {
            DataTable dt = await ExecuteQuery("SELECT spellName FROM spells_dbc WHERE id = " + id);

            if (dt.Rows.Count == 0)
                return "_replaceBecauseOfError_<Spell '" + id + "' not found!>";

            return dt.Rows[0]["spellName"].ToString();
        }

        public async Task<string> GetQuestNameById(int id)
        {
            DataTable dt = await ExecuteQuery("SELECT title FROM quest_template WHERE id = " + id);

            if (dt.Rows.Count == 0)
                return "_replaceBecauseOfError_<Quest '" + id + "' not found!>";

            return dt.Rows[0]["title"].ToString();
        }

        public async Task<string> GetQuestNameForCastedByCreatureOrGo(int requiredNpcOrGo1, int requiredNpcOrGo2, int requiredNpcOrGo3, int requiredNpcOrGo4, int requiredSpellCast1)
        {
            DataTable dt = await ExecuteQuery(String.Format("SELECT title FROM quest_template WHERE (RequiredNpcOrGo1 = {0} OR RequiredNpcOrGo2 = {1} OR RequiredNpcOrGo3 = {2} OR RequiredNpcOrGo4 = {3}) AND RequiredSpellCast1 = {4}", requiredNpcOrGo1, requiredNpcOrGo2, requiredNpcOrGo3, requiredNpcOrGo4, requiredSpellCast1));

            if (dt.Rows.Count == 0)
                return "_replaceBecauseOfError_<Quest not found (GetQuestNameForCastedByCreatureOrGo)!>";

            return dt.Rows[0]["title"].ToString();
        }

        public async Task<string> GetQuestNameForKilledMonster(int requiredNpcOrGo1, int requiredNpcOrGo2, int requiredNpcOrGo3, int requiredNpcOrGo4)
        {
            DataTable dt = await ExecuteQuery(String.Format("SELECT title FROM quest_template WHERE (RequiredNpcOrGo1 = {0} OR RequiredNpcOrGo2 = {1} OR RequiredNpcOrGo3 = {2} OR RequiredNpcOrGo4 = {3})", requiredNpcOrGo1, requiredNpcOrGo2, requiredNpcOrGo3, requiredNpcOrGo4));

            if (dt.Rows.Count == 0)
                return "_replaceBecauseOfError_<Quest not found (GetQuestNameForKilledMonster)!>";

            return dt.Rows[0]["title"].ToString();
        }

        public async Task<string> GetItemNameById(int id)
        {
            DataTable dt = await ExecuteQuery("SELECT name FROM item_template WHERE entry = " + id);

            if (dt.Rows.Count == 0)
                return "_replaceBecauseOfError_<Item '" + id + "' not found!>";

            return dt.Rows[0]["name"].ToString();
        }

        public async Task<List<Condition>> GetConditions()
        {
            DataTable dt = await ExecuteQuery("SELECT * FROM conditions");

            if (dt.Rows.Count == 0)
                return null;

            List<Condition> conditions = new List<Condition>();

            foreach (DataRow row in dt.Rows)
                conditions.Add(BuildCondition(row));

            return conditions;
        }

        private static Condition BuildCondition(DataRow row)
        {
            var condition = new Condition();
            condition.SourceTypeOrReferenceId = row["SourceTypeOrReferenceId"] != DBNull.Value ? Convert.ToInt32(row["SourceTypeOrReferenceId"]) : 0;
            condition.SourceGroup = row["SourceGroup"] != DBNull.Value ? Convert.ToInt32(row["SourceGroup"]) : 0;
            condition.SourceEntry = row["SourceEntry"] != DBNull.Value ? Convert.ToInt32(row["SourceEntry"]) : 0;
            condition.SourceId = row["SourceId"] != DBNull.Value ? Convert.ToInt32(row["SourceId"]) : 0;
            condition.ElseGroup = row["ElseGroup"] != DBNull.Value ? Convert.ToInt32(row["ElseGroup"]) : 0;
            condition.ConditionTypeOrReference = row["ConditionTypeOrReference"] != DBNull.Value ? Convert.ToInt32(row["ConditionTypeOrReference"]) : 0;
            condition.ConditionTarget = row["ConditionTarget"] != DBNull.Value ? Convert.ToInt32(row["ConditionTarget"]) : 0;
            condition.ConditionValue1 = row["ConditionValue1"] != DBNull.Value ? Convert.ToInt32(row["ConditionValue1"]) : 0;
            condition.ConditionValue2 = row["ConditionValue2"] != DBNull.Value ? Convert.ToInt32(row["ConditionValue2"]) : 0;
            condition.ConditionValue3 = row["ConditionValue3"] != DBNull.Value ? Convert.ToInt32(row["ConditionValue3"]) : 0;
            condition.NegativeCondition = row["NegativeCondition"] != DBNull.Value ? Convert.ToInt32(row["NegativeCondition"]) : 0;
            condition.ErrorType = row["ErrorType"] != DBNull.Value ? Convert.ToInt32(row["ErrorType"]) : 0;
            condition.ErrorTextId = row["ErrorTextId"] != DBNull.Value ? Convert.ToInt32(row["ErrorTextId"]) : 0;
            condition.ScriptName = row["ScriptName"] != DBNull.Value ? (string)row["ScriptName"] : String.Empty;
            condition.Comment = row["Comment"] != DBNull.Value ? (string)row["Comment"] : String.Empty;
            return condition;
        }
    }
}
