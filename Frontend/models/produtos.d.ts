export interface IProduto {
  id: number;
  nome: string;
  preco: number;
  quantidade: number;
}

export interface sendProduto {
  nome: string;
  preco: number;
  quantidade: number;
}

export interface IProdutos {
  produtos: IProdutos[];
}
