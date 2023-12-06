export interface IFuncionario {
  id: number;
  nome: string;
  cpf: string;
}

export interface sendFuncionario {
  nome: string;
  cpf: string;
}

export interface IFuncionarios {
  funcionarios: IFuncionario[];
}
