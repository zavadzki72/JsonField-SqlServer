
# JsonFields EF Core 7

Usando campos JSON em colunas do SQLServer no EFCore 7


## Dependencias
- [.NET 6](https://dotnet.microsoft.com/download)
- [SqlServer](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [EF Core 7](https://learn.microsoft.com/en-us/ef/)
- [Docker](https://www.docker.com) *


> *Você não precisa necessáriamente ter o docker instalado, mas você consegue subir o SQLServer usando a `docker-compose.yml`.


Clone o projeto
```bash
  git clone https://github.com/zavadzki72/JsonField-SqlServer.git
```

Entre no diretório do projeto
```bash
  cd JsonField-SqlServer
```

Suba o docker-compose
```bash
  docker-compose up -d
```

Atualize o banco com as migrations
```bash
  Update-Database -StartupProject Negociacoes.WebApi -Project Negociacoes.WebApi -Context ApplicationContext
```

Entre no diretório do projeto API
```bash
  cd .\Negociacoes\Negociacoes.WebApi\
```

Suba a aplicação
```bash
  dotnet run
```

## Funcionamento

### Step1: Criação da entidade

Para conseguir criar colunas JSON (Que no fundo dentro do SQL viram um `nvarchar(max)`)

![coluna-no-banco](https://github.com/zavadzki72/JsonField-SqlServer/assets/33812121/f3ae24e7-cad6-4c40-8345-68ffc6f1ed30)

Precisamos apenas criar a nossa entidade com o mapeamento da forma correta

![NegociacaoComposicaoCargaTab](https://github.com/zavadzki72/JsonField-SqlServer/assets/33812121/ea041c7f-95af-4a16-8da8-d37d19a13a81)

No meu caso tenho a tabela `NegociacaoComposicaoCarga` e a coluna que quero transformar em JSON será a `MetaData`
Perceba que o tipo do meu atributo é uma classe criada por mim mesmo

![NegociacaoComposicaoCargaJson](https://github.com/zavadzki72/JsonField-SqlServer/assets/33812121/35b988a8-edd2-4379-8d63-edfc21aac6e5)

Essa classe nada mais é do que a estrutura do JSON nela eu tenho os atributos necessários para a geração do mesmo

![tipos](https://github.com/zavadzki72/JsonField-SqlServer/assets/33812121/1b079bf7-749c-4945-9932-ebf920a764b1)

Perceba que foi necessario criar tipos complexos para um simples (`IntPedido`, `IntSugestao`), isso porque o EF nao suporta tipos que não sejam class, por isso foi necessario criar essas classes ao inves de simplesmente usar duas `List<int>`

### Step2: Mapeamento da entidade

Agora que já temos a nossa entidade criada, vamos realizar o mapeamento da mesma, no meu caso, gosto de usar classes de mapeamento separadas (`NegociacaoComposicaoCargaMap`)

![NegociacaoComposicaoCargaMap](https://github.com/zavadzki72/JsonField-SqlServer/assets/33812121/d8b33921-de9d-4ab3-be2f-60572b9d8ea1)

Dentro dela é que a magia acontece, mais especificamente nesse trecho de codigo

```csharp
builder.OwnsOne(
  negociacao => negociacao.MetaData, ownedNavigationBuilder => {
    ownedNavigationBuilder.ToJson("meta_data");
    ownedNavigationBuilder.OwnsMany(metadata => metadata.PedidosAtuais).HasJsonPropertyName("pedidos_atuais");
    ownedNavigationBuilder.OwnsMany(metadata => metadata.PedidosNovos).HasJsonPropertyName("pedidos_novos");
    ownedNavigationBuilder.OwnsMany(metadata => metadata.PedidosRemovidos).HasJsonPropertyName("pedidos_removidos");
    ownedNavigationBuilder.OwnsMany(metadata => metadata.SugestoesNovas).HasJsonPropertyName("sugestoes_novas");
    ownedNavigationBuilder.OwnsMany(metadata => metadata.SugestoesGeradasPorNegociacao).HasJsonPropertyName("sugestoes_geradas_por_negociacao");
  }
);
```

Aqui nos estamos avisando o EF que a coluna MetaData sera um json (`ToJson`), e também falamos como JSON será estruturado

Lembra quando eu disse que nao era possivel criar uma `List<int>` e usar pra estrutura do JSON? é por isso aqui

![classObrigatoria](https://github.com/zavadzki72/JsonField-SqlServer/assets/33812121/2b01aa70-aba0-41eb-b2d0-abf6244795ee)

Depois dessa configuração e só adicionar a migration e correr pro abraço! E como eu disse no começo no fundo no fundo é tudo um `nvarchar(max)`

![nvarchartwo](https://github.com/zavadzki72/JsonField-SqlServer/assets/33812121/0676378e-7f2e-4130-a06a-949ecad4ab4b)

### Step3: Queries

A forma de escrita de query via código não muda absolutamente nada, aqui vai um exemplo

```csharp
var negociacao = await _applicationContext.Set<NegociacaoComposicaoCarga>()
  .Include(x => x.ComposicaoCarga)
  .FirstOrDefaultAsync(x => x.Id == negotiationId && x.IdComposicaoCarga == loadCompositionId);
```

O que muda é na tradução que o EF faz

```sql
SELECT TOP(1) [n].[id], [n].[data_evento], [n].[id_composicao_carga], [n].[observacao], [n].[tipo_negociacao], [n].[tipo_usuario_responsavel_proxima_etapa], JSON_QUERY([n].[meta_data],'$'), [c].[id], [c].[data_entrega], [c].[id_usuario], [c].[observacao], [c].[situacao]
      FROM [negociacao_composicao_carga] AS [n]
      INNER JOIN [composicao_carga] AS [c] ON [n].[id_composicao_carga] = [c].[id]
      WHERE [n].[id] = @__negotiationId_0 AND [n].[id_composicao_carga] = @__loadCompositionId_1
```

Perceba que ele usa a ferramenta `JSON_QUERY` do SQLServer

## Referências

 - [EF JSON Columns](https://devblogs.microsoft.com/dotnet/announcing-ef7-release-candidate-2/)
 - [Dados JSON no SqlServer](https://learn.microsoft.com/pt-br/sql/relational-databases/json/json-data-sql-server?view=sql-server-ver16)

## Feedback

Se você tiver algum feedback, por favor entre em [contato](https://marccusz.com) =)
