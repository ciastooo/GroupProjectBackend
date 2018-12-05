using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupProjectBackend.Models.DB.Migrations
{
    public partial class CategoriesInserts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"insert into Categories (Name, Description) values ('restaurant', 'Restauracja');
                                   insert into Categories (Name, Description) values ('hair', 'Fryzjer');
                                   insert into Categories (Name, Description) values ('entertainment', 'Rozrywka');
                                   insert into Categories (Name, Description) values ('relax', 'Relaks/Spa');
                                   insert into Categories (Name, Description) values ('viewPoint', 'Punkt widokowy');");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
