using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using SyF.Models;

namespace SyF.Migrations
{
    [DbContext(typeof(SyFContext))]
    [Migration("20170326200223_InitialDatabase")]
    partial class InitialDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SyF.Models.Ingredient", b =>
                {
                    b.Property<int>("IngredientID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DisplayIndex");

                    b.Property<string>("Name");

                    b.Property<double>("Quantity");

                    b.Property<int?>("RecipeId");

                    b.HasKey("IngredientID");

                    b.HasIndex("RecipeId");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("SyF.Models.Recipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("Name");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("SyF.Models.Ingredient", b =>
                {
                    b.HasOne("SyF.Models.Recipe")
                        .WithMany("Ingredients")
                        .HasForeignKey("RecipeId");
                });
        }
    }
}
