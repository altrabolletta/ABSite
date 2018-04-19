namespace ABSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    //public partial class InitialCreate : DbMigration
    //{
    //    public override void Up()
    //    {
    //        CreateTable(
    //            "usr.Role",
    //            c => new
    //                {
    //                    RoleId = c.Long(nullable: false, identity: true),
    //                    Description = c.String(),
    //                    CreatedDate = c.DateTime(nullable: false),
    //                    Name = c.String(),
    //                })
    //            .PrimaryKey(t => t.RoleId);
            
    //        CreateTable(
    //            "usr.UserRole",
    //            c => new
    //                {
    //                    UserId = c.Long(nullable: false),
    //                    RoleId = c.Long(nullable: false),
    //                })
    //            .PrimaryKey(t => new { t.UserId, t.RoleId })
    //            .ForeignKey("usr.Role", t => t.RoleId, cascadeDelete: true)
    //            .ForeignKey("usr.User", t => t.UserId, cascadeDelete: true)
    //            .Index(t => t.UserId)
    //            .Index(t => t.RoleId);
            
    //        CreateTable(
    //            "usr.User",
    //            c => new
    //                {
    //                    UserId = c.Long(nullable: false, identity: true),
    //                    FirstName = c.String(),
    //                    LastName = c.String(),
    //                    CreatedDate = c.DateTime(nullable: false),
    //                    Email = c.String(),
    //                    EmailConfirmed = c.Boolean(nullable: false),
    //                    PasswordHash = c.String(),
    //                    SecurityStamp = c.String(),
    //                    PhoneNumber = c.String(),
    //                    PhoneNumberConfirmed = c.Boolean(nullable: false),
    //                    TwoFactorEnabled = c.Boolean(nullable: false),
    //                    LockoutEndDateUtc = c.DateTime(),
    //                    LockoutEnabled = c.Boolean(nullable: false),
    //                    AccessFailedCount = c.Int(nullable: false),
    //                    UserName = c.String(),
    //                })
    //            .PrimaryKey(t => t.UserId);
            
    //        CreateTable(
    //            "usr.UserClaim",
    //            c => new
    //                {
    //                    UserClaimId = c.Int(nullable: false, identity: true),
    //                    UserId = c.Long(nullable: false),
    //                    ClaimType = c.String(),
    //                    ClaimValue = c.String(),
    //                })
    //            .PrimaryKey(t => t.UserClaimId)
    //            .ForeignKey("usr.User", t => t.UserId, cascadeDelete: true)
    //            .Index(t => t.UserId);
            
    //        CreateTable(
    //            "usr.UserLogin",
    //            c => new
    //                {
    //                    LoginProvider = c.String(nullable: false, maxLength: 128),
    //                    ProviderKey = c.String(nullable: false, maxLength: 128),
    //                    UserId = c.Long(nullable: false),
    //                })
    //            .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
    //            .ForeignKey("usr.User", t => t.UserId, cascadeDelete: true)
    //            .Index(t => t.UserId);
            
    //    }
        
    //    public override void Down()
    //    {
    //        DropForeignKey("usr.UserRole", "UserId", "usr.User");
    //        DropForeignKey("usr.UserLogin", "UserId", "usr.User");
    //        DropForeignKey("usr.UserClaim", "UserId", "usr.User");
    //        DropForeignKey("usr.UserRole", "RoleId", "usr.Role");
    //        DropIndex("usr.UserLogin", new[] { "UserId" });
    //        DropIndex("usr.UserClaim", new[] { "UserId" });
    //        DropIndex("usr.UserRole", new[] { "RoleId" });
    //        DropIndex("usr.UserRole", new[] { "UserId" });
    //        DropTable("usr.UserLogin");
    //        DropTable("usr.UserClaim");
    //        DropTable("usr.User");
    //        DropTable("usr.UserRole");
    //        DropTable("usr.Role");
    //    }
    //}
}
