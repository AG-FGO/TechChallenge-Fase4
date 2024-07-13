using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge_Data.Data;
using TechChallenge_Domain.Entities;
using TechChallenge_Domain.Interfaces;

namespace TechChallenge_Data.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class CarteiraRepository : ComumRepository<Carteira>, ICarteiraRepository
    {
        protected ApplicationDbContext _dbContext;

        public CarteiraRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void AdicionarValorCarteira(int id, float valor)
        {
            var existeCarteira = _dbContext.Carteira.Where(c => c.UsuarioId == id).Any();

            if (!existeCarteira)
            {
                var carteira = new Carteira();
                carteira.UsuarioId = id;
                AtribuirSaldo(carteira, valor);
                _dbContext.Carteira.Add(carteira);
                _dbContext.SaveChanges();
                return;
            }
            else
            {
                var carteira = _dbContext.Carteira.Where(c => c.UsuarioId == id).FirstOrDefault();
                AdicinarSaldo(carteira, valor);
                _dbContext.SaveChanges();
            }
        }

        public bool ComprarAcoes(int IdUsuario, int IdAcao, int quantidade, bool enviaServiceBus = false)
        {
            var carteira = GetCarteiraByUsuarioID(IdUsuario);

            if (carteira == null)
                return false;

            var acao = _dbContext.Acao.Where(a => a.Id == IdAcao).FirstOrDefault();

            if (acao == null)
                return false;

            if (enviaServiceBus)
            {
                /*_serviceBusRepository.ComprarAcoes(new ComprarAcoesServiceBus
                {
                    Quantidade = quantidade,
                    Carteira = carteira,
                    Acao = acao
                });

                return true;*/
                return true;
            }
            else
            {
                if (carteira.Saldo < (acao.Valor * quantidade))
                    return false;

                if (carteira.Acoes != null)
                {
                    if (carteira.Acoes.Where(w => w.Acao.Id == IdAcao).FirstOrDefault() != null)
                    {
                        carteira.Acoes.Where(w => w.Acao.Id == IdAcao).FirstOrDefault().Quantidade += quantidade;
                        RemoverSaldo(carteira, carteira.Acoes.Where(w => w.Acao.Id == IdAcao).FirstOrDefault().Acao.Valor * quantidade);
                        _dbContext.Entry(carteira.Acoes.Where(w => w.Acao.Id == IdAcao).FirstOrDefault()).State = EntityState.Modified;
                        _dbContext.Entry(carteira).State = EntityState.Modified;
                        _dbContext.Carteira.Update(carteira);
                        _dbContext.SaveChanges();
                        return true;
                    }
                    else
                    {
                        carteira.Acoes.Add(new Ativos()
                        {
                            Acao = acao,
                            Quantidade = quantidade,
                            DataCompra = System.DateTime.Now
                        });

                        RemoverSaldo(carteira, acao.Valor * quantidade);
                        _dbContext.Carteira.Update(carteira);
                        _dbContext.SaveChanges();
                        return true;
                    }

                }
                else
                {
                    carteira.Acoes.Add(new Ativos()
                    {
                        Acao = acao,
                        Quantidade = quantidade,
                        DataCompra = System.DateTime.Now
                    });

                    RemoverSaldo(carteira, acao.Valor * quantidade);
                    _dbContext.Carteira.Update(carteira);
                    _dbContext.SaveChanges();
                    return true;
                }
            }
        }

        public Carteira GetCarteiraByUsuarioID(int id)
        {
            return _dbContext.Carteira
                .Include(Carteira => Carteira.Acoes)
                .ThenInclude(Ativos => Ativos.Acao)
                .AsNoTracking()
                .Where(c => c.UsuarioId == id)
                .ToList()
                .Select(carteira =>
                {
                    if (carteira.Acoes != null)
                        carteira.Acoes = carteira.Acoes.Select(a => new Ativos
                        {
                            Id = a.Id,
                            DataCompra = a.DataCompra,
                            Quantidade = a.Quantidade,
                            IdCarteira = a.IdCarteira,
                            Acao = new Acao
                            {
                                Id = a.Acao.Id,
                                Nome = a.Acao.Nome,
                                Valor = a.Acao.Valor
                            }
                        }).ToList();
                    return carteira;
                })
                .FirstOrDefault();
        }

        public bool RemoverValorCarteira(int id, float valor)
        {
            var existeCarteira = _dbContext.Carteira.Where(c => c.UsuarioId == id).Any();

            if (!existeCarteira)
                return false;

            var carteira = _dbContext.Carteira.Where(c => c.UsuarioId == id).FirstOrDefault();
            RemoverSaldo(carteira, valor);
            _dbContext.SaveChanges();
            return true;
        }

        public bool VenderAcoes(int IdUsuario, int IdAcao, int quantidade)
        {
            try
            {
                var carteira = GetCarteiraByUsuarioID(IdUsuario);

                if (carteira == null)
                    return false;

                if (carteira.Acoes.Where(w => w.Acao.Id == IdAcao).FirstOrDefault() != null)
                {
                    if (quantidade > carteira.Acoes.Where(w => w.Acao.Id == IdAcao).FirstOrDefault().Quantidade)
                        return false;

                    var valorVendido = carteira.Acoes.Where(w => w.Acao.Id == IdAcao).FirstOrDefault().Acao.Valor * quantidade;

                    if (quantidade == carteira.Acoes.Where(w => w.Acao.Id == IdAcao).FirstOrDefault().Quantidade)
                    {
                        var acaoRemover = carteira.Acoes.Where(w => w.Acao.Id == IdAcao).FirstOrDefault();
                        _dbContext.Ativos.Remove(acaoRemover);
                    }
                    else
                        carteira.Acoes.Where(w => w.Acao.Id == IdAcao).FirstOrDefault().Quantidade -= quantidade;

                    AdicinarSaldo(carteira, valorVendido);
                    _dbContext.Carteira.Update(carteira);
                    _dbContext.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public float RecuperarSaldo(Carteira carteira)
        {
            return carteira.Saldo;
        }

        public void AtribuirSaldo(Carteira carteira, float saldoAtribuir)
        {
            carteira.Saldo = saldoAtribuir;
        }

        public void AdicinarSaldo(Carteira carteira, float saldoAdicionar)
        {
            carteira.Saldo += saldoAdicionar;
        }

        public void RemoverSaldo(Carteira carteira, float saldoRemover)
        {
            carteira.Saldo -= saldoRemover;
        }
    }
}
