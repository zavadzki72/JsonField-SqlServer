namespace Negociacoes.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first_ef_six : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.composicao_carga",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdUsuario = c.Int(nullable: false),
                        DataEntrega = c.DateTime(nullable: false),
                        Situacao = c.Int(nullable: false),
                        Observacao = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.usuario", t => t.IdUsuario)
                .Index(t => t.IdUsuario);
            
            CreateTable(
                "dbo.usuario",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        TipoUsuario = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.negociacao_composicao_carga",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DataEvento = c.DateTime(nullable: false),
                        IdComposicaoCarga = c.Int(nullable: false),
                        Observacao = c.String(),
                        TipoNegociacao = c.Int(nullable: false),
                        TipoUsuarioResponsavelProximaEtapa = c.Int(nullable: false),
                        MetaData = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.composicao_carga", t => t.IdComposicaoCarga)
                .Index(t => t.IdComposicaoCarga);
            
            CreateTable(
                "dbo.pedido",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdComposicaoCarga = c.Int(),
                        DataEntrega = c.DateTime(nullable: false),
                        Quantidade = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Item = c.String(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.composicao_carga", t => t.IdComposicaoCarga)
                .Index(t => t.IdComposicaoCarga);
            
            CreateTable(
                "dbo.sugestao",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DataEntrega = c.DateTime(nullable: false),
                        Quantidade = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Item = c.String(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.pedido", "IdComposicaoCarga", "dbo.composicao_carga");
            DropForeignKey("dbo.negociacao_composicao_carga", "IdComposicaoCarga", "dbo.composicao_carga");
            DropForeignKey("dbo.composicao_carga", "IdUsuario", "dbo.usuario");
            DropIndex("dbo.pedido", new[] { "IdComposicaoCarga" });
            DropIndex("dbo.negociacao_composicao_carga", new[] { "IdComposicaoCarga" });
            DropIndex("dbo.composicao_carga", new[] { "IdUsuario" });
            DropTable("dbo.sugestao");
            DropTable("dbo.pedido");
            DropTable("dbo.negociacao_composicao_carga");
            DropTable("dbo.usuario");
            DropTable("dbo.composicao_carga");
        }
    }
}
