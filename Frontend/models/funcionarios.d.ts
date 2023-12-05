export interface IFuncionario {
  id: number;
  nome: string;
  CPF: string;
}

export interface sendFuncionario {
  nome: string;
  CPF: string;
}

export interface IFuncionarios {
  funcionarios: IFuncionario[];
}
