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
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("*");
        });
});


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
    var nAssets = random.Next(100, 100000);
    for(int i=0; i< nAssets; i++)
    {
        var index = random.Next(0, assetListType.Count);
        allAssetList.Add(assetListType[index]);
    }
}


GenerateAllAssetList();


var app = builder.Build();
app.UseCors();

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
    var availableAssets = allAssetList.Where(x => x.IsAvailable()).ToList();
    var res = Paginate(allAssetList, page);
    if(res.assets.Count < 1)
    {
        return Results.NoContent();
    }
    return Results.Ok(res);
});

app.MapGet("/assets/{guid:guid}", (Guid guid) =>
{
    var asset = allAssetList.FirstOrDefault(x => x.Guid.CompareTo(guid) == 0);
    if (asset is null)
    {
        return Results.NoContent();
    }


    var random = new Random();
    var res = random.Next(1, 8);

    if (res == 3)
    {
        return Results.NoContent();
    }

    
    return Results.Ok(asset);
});

/*
app.MapGet("/{companyName}/assets", (string companyName, int? page) =>
{
    var res = GetAssetsByCompanyName(companyName, page);
    if(res.assets.Count < 1)
    {
        return Results.NoContent();
    }
    return Results.Ok(res);
});

*/

/*
PaginetedReturn GetAssetsByCompanyName(string companyName,int? page)
{
    if(page is null)
    {
        page = 1;
    }
    var assets = allAssetList.Where(x => x.CompanyName.Equals(companyName)).ToList();
    return Paginate(assets, (int)page);
}
*/

PaginetedReturn Paginate(List<Asset> assetsList,int? pageParam)
{
    int page = 1;
    if(pageParam is not null)
    {
        page = (int)pageParam;
    }
    var rollsPerPage = 25;
    var assets = assetsList.Skip(rollsPerPage * (page - 1)).Take(rollsPerPage).ToList();
    var res = new PaginetedReturn { 
        assets = assets,
        rollsPerPage = rollsPerPage,
        count = assets.Count,
        page = page,
    };
    return res;
}

app.Run();
