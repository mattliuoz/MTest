using FluentMigrator;

namespace MTestDB
{
    [Migration(20181230000000)]
    public class InitData: Migration
    {
        public override void Up()
        {
            var fileName = @"2018_12_30_InitData.sql";
            Execute.Script(fileName);
        }

        public override void Down()
        {
            //NOTHING TO DO HERE
        }
    }

}