using FluentMigrator;

namespace MyRecepiBook.Infrastructure.Migrations.Versions;

[Migration(DatabaseVersions.IMAGES_FOR_RECIPES, "Add column on recipe table to save images")]
public class Version0000003 : VersionBase
{
    public override void Up()
    {
        Alter.Table("Recipes").AddColumn("ImageIdentifier").AsString().Nullable();
    }
}