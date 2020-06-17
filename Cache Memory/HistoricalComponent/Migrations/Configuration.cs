namespace HistoricalComponent.Migrations
{
    using ModelsAndProps.Historical;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<HistoricalComponent.DatabaseConn.Database>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(HistoricalComponent.DatabaseConn.Database context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            IList<ListDescription> listDescriptions = new List<ListDescription>();

            listDescriptions.Add(new ListDescription { Id = 1, HistoricalDescriptions = new List<HistoricalDescription>() });
            listDescriptions.Add(new ListDescription { Id = 2, HistoricalDescriptions = new List<HistoricalDescription>() });
            listDescriptions.Add(new ListDescription { Id = 3, HistoricalDescriptions = new List<HistoricalDescription>() });
            listDescriptions.Add(new ListDescription { Id = 4, HistoricalDescriptions = new List<HistoricalDescription>() });
            listDescriptions.Add(new ListDescription { Id = 5, HistoricalDescriptions = new List<HistoricalDescription>() });

            context.ListDescriptions.AddRange(listDescriptions);

            base.Seed(context);
        }
    }
}
