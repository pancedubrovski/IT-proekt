namespace proekt.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dodadeniTriAtributiVoTelefon : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Telefons", "ekran", c => c.Single(nullable: false));
            AddColumn("dbo.Telefons", "procesor", c => c.String(nullable: false));
            AddColumn("dbo.Telefons", "RAM", c => c.Int(nullable: false));
            AddColumn("dbo.Telefons", "kamera", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Telefons", "kamera");
            DropColumn("dbo.Telefons", "RAM");
            DropColumn("dbo.Telefons", "procesor");
            DropColumn("dbo.Telefons", "ekran");
        }
    }
}
