export interface ICliente {
  id: number;
  nome: string;
  CPF: string;
}

export interface sendCliente {
  nome: string;
  CPF: string;
}

export interface IClientes {
  clientes: IClientes[];
}
