namespace OutreachCli
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services) => services.AddMvc();
        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.UseRouting();
            app.UseEndpoints(x => x.MapControllers());
        }
    }
}