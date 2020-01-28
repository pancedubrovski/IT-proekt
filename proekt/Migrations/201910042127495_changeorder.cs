namespace proekt.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeorder : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cards",
                c => new
                    {
                        Recordid = c.Int(nullable: false, identity: true),
                        cardId = c.String(nullable: false),
                        TelefonId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                        DataCreate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Recordid)
                .ForeignKey("dbo.Telefons", t => t.TelefonId, cascadeDelete: true)
                .Index(t => t.TelefonId);
            
            CreateTable(
                "dbo.Telefons",
                c => new
                    {
                        TelefonID = c.Int(nullable: false, identity: true),
                        ImeTelefon = c.String(nullable: false),
                        cena = c.Int(nullable: false),
                        slika = c.String(),
                        proID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TelefonID)
                .ForeignKey("dbo.Proizvoditels", t => t.proID, cascadeDelete: true)
                .Index(t => t.proID);
            
            CreateTable(
                "dbo.Proizvoditels",
                c => new
                    {
                        proID = c.Int(nullable: false, identity: true),
                        ime = c.String(),
                    })
                .PrimaryKey(t => t.proID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        State = c.String(),
                        PostalCode = c.String(),
                        Country = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrderDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId);
            
            CreateTable(
                "dbo.OrederDetals",
                c => new
                    {
                        OrderDetailId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        TelefonId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.OrderDetailId)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Telefons", t => t.TelefonId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.TelefonId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.OrederDetals", "TelefonId", "dbo.Telefons");
            DropForeignKey("dbo.OrederDetals", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Cards", "TelefonId", "dbo.Telefons");
            DropForeignKey("dbo.Telefons", "proID", "dbo.Proizvoditels");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.OrederDetals", new[] { "TelefonId" });
            DropIndex("dbo.OrederDetals", new[] { "OrderId" });
            DropIndex("dbo.Telefons", new[] { "proID" });
            DropIndex("dbo.Cards", new[] { "TelefonId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.OrederDetals");
            DropTable("dbo.Orders");
            DropTable("dbo.Proizvoditels");
            DropTable("dbo.Telefons");
            DropTable("dbo.Cards");
        }
    }
}
