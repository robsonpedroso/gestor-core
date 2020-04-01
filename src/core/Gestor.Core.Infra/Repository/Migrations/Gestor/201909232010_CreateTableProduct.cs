using FluentMigrator;

namespace Gestor.Core.Infra.Repository.Migrations.Gestor
{
    [Migration(201909232010), Tags("Gestor")]
    public class CreateTableProduct : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("product")
                .WithColumn("id").AsGuid().PrimaryKey().NotNullable()
                .WithColumn("name").AsString(250).NotNullable()
                .WithColumn("description").AsString(2500).NotNullable()
                .WithColumn("object_json").AsCustom("varchar(max)")
                .WithColumn("removed").AsInt32().NotNullable()
                .WithColumn("created_at").AsDateTime().NotNullable()
                .WithColumn("updated_at").AsDateTime().NotNullable();
        }
    }
}
