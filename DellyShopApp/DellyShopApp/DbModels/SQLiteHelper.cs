using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DellyShopApp.DbModels
{
    public class SQLiteHelper
    {
        SQLiteAsyncConnection db;
        public SQLiteHelper(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<Tbl_ProductResponse>().Wait();
        }

        public Task<int> SaveItemAsync(List<Tbl_ProductResponse> db_Product)
        {
            return db.InsertAllAsync(db_Product);
        }
        public async Task<List<Tbl_ProductResponse>> GetItemsAsync(bool IsHorizontal = true, int pageIndex = 0, int pageSize = 20)
        {
            if (IsHorizontal)
                return await db.Table<Tbl_ProductResponse>().Where(x => !string.IsNullOrEmpty(x.status)).ToListAsync();
            else
            {
                var prodResponse = await db.Table<Tbl_ProductResponse>().Where(x => x.status == string.Empty).ToListAsync();
                prodResponse = prodResponse.Skip(pageIndex * pageSize).Take(pageSize).ToList();
                return prodResponse;
            }
        }
    }
}
