export interface ICliente {
  id: number;
  nome: string;
  cpf: string;
}

export interface sendCliente {
  nome: string;
  cpf: string;
}

export interface IClientes {
  clientes: IClientes[];
}
