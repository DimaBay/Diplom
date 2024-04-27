using inventory.Interfaces;
using inventory.ViewModel;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace inventory
{
	public class Startup
	{
		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddTransient<IEquipment, DBEquipment>();
			services.AddTransient<IUsers, DBUsers>();
			services.AddTransient<DBProfile>();
			services.AddTransient<DBHistory>();

			services.AddMvc(option => option.EnableEndpointRouting = false);
			services.AddSession();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseSession();
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();

				app.UseStaticFiles();

				app.UseStatusCodePages();

				app.UseMvcWithDefaultRoute();
			}
			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapGet("/", async context =>
				{
					await context.Response.WriteAsync("Hello World!");
				});
			});
		}
	}
}
