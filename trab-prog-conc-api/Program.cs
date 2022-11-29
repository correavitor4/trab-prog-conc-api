using Nancy.Json;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using trab_prog_conc_api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var assetListType = new List<Asset>();

assetListType.Add(new Asset(companyName: "Compania de Petroleo do Rio de Janeiro", symbol: "CPPR3"));
assetListType.Add(new Asset(companyName: "Compania de Carne Aiai", symbol: "CARN3"));
assetListType.Add(new Asset(companyName: "Mineradora Corrêa", symbol: "MCRR3"));
assetListType.Add(new Asset(companyName: "Compania Elétrica AB", symbol: "CPEL3"));
assetListType.Add(new Asset(companyName: "Compania Elétrica BA", symbol: "CPAE3"));
assetListType.Add(new Asset(companyName: "Compania Eólica", symbol: "CPEO3"));
assetListType.Add(new Asset(companyName: "Compania Eólica", symbol: "CPEL4"));
assetListType.Add(new Asset(companyName: "Compania de Eletrodomésticos", symbol: "CELD3"));
assetListType.Add(new Asset(companyName: "Compania de Eletrodomésticos", symbol: "CELD4"));
assetListType.Add(new Asset(companyName: "Compania de Aviação", symbol: "CAVA4"));
assetListType.Add(new Asset(companyName: "Compania de Aviação", symbol: "CAVA3"));

var allAssetList = new List<Asset>();

void GenerateAllAssetList()
{
    var random = new Random();
    foreach (var ast in assetListType)
    {
        var nAssets = random.Next(100, 10000);
        for (int i = 0; i < nAssets; i++)
        {
            allAssetList.Add(ast);
        }
    }
}

GenerateAllAssetList();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () =>
{
    return "Hello World";
});

app.MapGet("/assets", (int? page) => {
    var ret = Paginate(assetsList: allAssetList, page);
    var res = JsonConvert.SerializeObject(ret);
    return Results.Ok(res);
});


PaginetedReturn Paginate(List<Asset> assetsList,int? pageParam)
{
    int page = 1;
    if(pageParam is not null)
    {
        page = (int)pageParam;
    }
    var rollsPerPage = 25;
    var assets = assetsList.Skip(rollsPerPage * (page - 1)).Take(rollsPerPage).ToList();
    return new PaginetedReturn { 
        assets = assets,
        rollsPerPage = rollsPerPage,
        count = assets.Count,
        page = page,
    };
}

app.Run();
