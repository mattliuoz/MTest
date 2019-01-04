using FluentMigrator;

namespace MTestDB
{
    [Migration(20181224000000)]
    public class AddCustomerTable : Migration
    {
        public override void Up()
        {
            Create.Table("Customers")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("FirstName").AsString()
                .WithColumn("LastName").AsString()
                .WithColumn("Email").AsString()
                .WithColumn("DOB").AsDateTime();
        }

        public override void Down()
        {
            Delete.Table("Customers");
        }
    }

}
