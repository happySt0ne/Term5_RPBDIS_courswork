using Microsoft.AspNetCore.Http;

namespace Term5_RPBDIS_mainLogic.sessionStuff {
    public class SearchSession {
        public string tableName;
        public string columnName;
        public string textForSearch;
        public bool isSaved = false;

        public void Save(string table, string column, string text, HttpContext context) {
            tableName = table;
            columnName = column;
            textForSearch = text;
            isSaved = true;

            context.Session.Set("searchSession", this);
        }
    }
}
