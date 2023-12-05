export interface IVenda {
  id: number;
  idCliente: number;
  idFuncionario: number;
  nome: string;
  quantidade: number;
}

export interface sendVenda {
  idCliente: number;
  idFuncionario: number;
  nome: string;
  quantidade: number;
}

export interface IVendas {
  vendas: IVenda[];
}
