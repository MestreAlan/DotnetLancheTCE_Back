
using LancheTCE_Back.models;
using Microsoft.EntityFrameworkCore;

namespace LancheTCE.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ProdutoPedido> PedidosProdutos { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>()
                .HasOne(p => p.UsuarioVendedor)
                .WithMany(u => u.ProdutosVendidos)
                .HasForeignKey(p => p.IdUsuarioVendedor);

            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Endereco)
                .WithMany(e => e.Usuarios)
                .HasForeignKey(u => u.IdEndereco)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.UsuarioVendedor)
                .WithMany(u => u.PedidosComoVendedor)
                .HasForeignKey(p => p.IdUsuarioVendedor);

            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.UsuarioCliente)
                .WithMany(u => u.PedidosComoCliente)
                .HasForeignKey(p => p.IdUsuarioCliente);

            modelBuilder.Entity<ProdutoPedido>()
                .HasOne(pp => pp.Produto)
                .WithMany()
                .HasForeignKey(pp => pp.IdProduto);

            modelBuilder.Entity<ProdutoPedido>()
                .HasOne(pp => pp.Pedido)
                .WithMany(p => p.PedidosProdutos)
                .HasForeignKey(pp => pp.IdPedido);

            base.OnModelCreating(modelBuilder);
        }
    }
}