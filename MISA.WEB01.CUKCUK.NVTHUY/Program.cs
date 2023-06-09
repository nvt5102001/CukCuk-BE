﻿using Microsoft.Extensions.FileProviders;
using MISA.WEB01.CUKCUK.NVTHUY.BL.Interfaces;
using MISA.WEB01.CUKCUK.NVTHUY.BL.Services;
using MISA.WEB01.CUKCUK.NVTHUY.Common.Entities;
using MISA.WEB01.CUKCUK.NVTHUY.DL.Interfaces;
using MISA.WEB01.CUKCUK.NVTHUY.DL.Repository;

var builder = WebApplication.CreateBuilder(args);

StaticFileOptions staticFileOptions = new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "UploadFile")),
    RequestPath = "/UploadFile"
};


builder.Services.AddControllers().AddJsonOptions(jsonOptions =>
{
    jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = null;
});



// Add services to the container.
builder.Services.AddScoped<IFoodDL, FoodDL>();
builder.Services.AddScoped<IFoodBL, FoodBL>();

builder.Services.AddScoped<IServiceHobbyDL, ServiceHobbyDL>();
builder.Services.AddScoped<IServiceHobbyBL, ServiceHobbyBL>();

builder.Services.AddScoped<IFoodDetailDL, FoodDetailDL>();
builder.Services.AddScoped<IFoodDetailBL, FoodDetailBL>();

builder.Services.AddScoped<IFoodUnitDL, FoodUnitDL>();
builder.Services.AddScoped<IFoodUnitBL, FoodUnitBL>();

builder.Services.AddScoped<ICookRoomDL, CookRoomDL>();
builder.Services.AddScoped<ICookRoomBL, CookRoomBL>();

builder.Services.AddScoped<IMenuGroupDL, MenuGroupDL>();
builder.Services.AddScoped<IMenuGroupBL, MenuGroupBL>();

builder.Services.AddScoped(typeof(IBaseBL<>), typeof(BaseBL<>));
builder.Services.AddScoped(typeof(IBaseDL<>), typeof(BaseDL<>));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Không bị định dạng json
builder.Services.AddControllers().AddJsonOptions(options =>
options.JsonSerializerOptions.PropertyNamingPolicy = null);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});

app.UseStaticFiles(staticFileOptions);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
