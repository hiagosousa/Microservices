import { useEffect, useState } from "react";
import reactLogo from "./assets/react.svg";
import viteLogo from "/vite.svg";
import { FaSync } from "react-icons/fa";
import { getClientes } from "../services/clientes";
import { getFuncionarios } from "../services/funcionarios";
import { getVendas } from "../services/vendas";
import { getProdutos } from "../services/produtos";
import "./App.css";
import { ICliente, IClientes } from "../models/clientes";
import { IFuncionario, IFuncionarios } from "../models/funcionarios";
import { IVenda, IVendas } from "../models/vendas";
import { IProdutos, IProduto } from "../models/produtos";
import Loading from "react-loading";

function App() {
  const [count, setCount] = useState(0);
  const [clientes, setClientes] = useState<ICliente[]>([]);
  const [funcionarios, setFuncionarios] = useState<IFuncionario[]>([]);
  const [vendas, setVendas] = useState<IVenda[]>([]);
  const [produtos, setProdutos] = useState<IProduto[]>([]);
  const [isLoading, setIsLoading] = useState(false);
  const [isSearched, setIsSearched] = useState(false);

  useEffect(() => {
    if (isSearched === false) {
      fetchClientes();
      fetchFuncionarios();
      fetchProdutos();
      fetchVendas();
    }
    setIsSearched(true);
  });

  const fetchClientes = async () => {
    setIsLoading(true);
    try {
      setClientes([]);
      const response = await getClientes();
      setClientes(response || []);
      console.log(response);
      setIsLoading(false);
    } catch (error) {
      setIsLoading(false);
      console.log(error);
    }
  };

  const fetchFuncionarios = async () => {
    setIsLoading(true);
    try {
      setFuncionarios([]);
      const response = await getFuncionarios();
      setFuncionarios(response || []);
      console.log(response);
      setIsLoading(false);
    } catch (error) {
      setIsLoading(false);
      console.log(error);
    }
  };

  const fetchProdutos = async () => {
    setIsLoading(true);
    try {
      setProdutos([]);
      const response = await getProdutos();
      setProdutos(response || []);
      console.log(response);
      setIsLoading(false);
    } catch (error) {
      setIsLoading(false);
      console.log(error);
    }
  };

  const fetchVendas = async () => {
    setIsLoading(true);
    try {
      setVendas([]);
      const response = await getVendas();
      setVendas(response || []);
      console.log(response);
      setIsLoading(false);
    } catch (error) {
      setIsLoading(false);
      console.log(error);
    }
  };

  return (
    <div className="flex flex-row bg-gray-400 w-screen h-screen p-4 justify-evenly items-center">
      <div
        className={`flex flex-col w-[22%] h-[80%] bg-slate-700 rounded-lg border-8 ${
          clientes.length > 0 ? "border-green-600" : "border-red-600"
        } justify-start items-center`}
      >
        <div className="flex flex-row w-full h-[15%] items-center justify-center">
          <span className="flex h-full w-[70%] text-gray-100 text-center items-center justify-center text-2xl">
            Clientes
          </span>
          <button
            className="flex h-[70%] w-[20%] text-gray-100 text-center items-center border-2 justify-center rounded-full"
            onClick={() => fetchClientes()}
          >
            <FaSync className="text-4xl" />
          </button>
        </div>

        <div className="flex flex-col w-[80%] h-[80%] overflow-y-scroll bg-gray-300 rounded-lg items-center">
          {!isLoading && clientes.length > 0 ? (
            <div className="flex flex-col w-[95%] h-[80%] space-y-2 mt-2">
              {clientes?.map((cliente) => (
                <div
                  key={cliente.id}
                  className="flex flex-col w-full bg-slate-600 items-center justify-center"
                >
                  <span className="flex h-full w-full text-gray-100 text-start items-center justify-center text-md">
                    id: {cliente.id}
                  </span>
                  <span className="flex h-full w-full text-gray-100 text-start items-center justify-center text-md">
                    Nome: {cliente.nome}
                  </span>
                  <span className="flex h-full w-full text-gray-100 text-start items-center justify-center text-md">
                    CPF: {cliente.cpf}
                  </span>
                </div>
              ))}
            </div>
          ) : !isLoading && clientes.length == 0 ? (
            <div className="bg-slate-600 mt-2">
              <span className="flex h-full w-full text-gray-100 text-center items-center justify-center text-xl">
                API indisponível, ou base sem dados
              </span>
            </div>
          ) : (
            <div>
              <Loading type="bars" color="#00BFFF"></Loading>
            </div>
          )}
        </div>
      </div>
      <div
        className={`flex flex-col w-[22%] h-[80%] bg-slate-700 rounded-lg border-8 ${
          funcionarios.length > 0 ? "border-green-600" : "border-red-600"
        } justify-start items-center`}
      >
        <div className="flex flex-row w-full h-[15%] items-center justify-center">
          <span className="flex h-full w-[70%] text-gray-100 text-center items-center justify-center text-2xl">
            Funcionários
          </span>
          <button
            className="flex h-[70%] w-[20%] text-gray-100 text-center items-center border-2 justify-center rounded-full"
            onClick={() => fetchFuncionarios()}
          >
            <FaSync className="text-4xl" />
          </button>
        </div>
        <div className="flex flex-col w-[80%] h-[80%] overflow-y-scroll bg-gray-300 rounded-lg items-center">
          {!isLoading && funcionarios.length > 0 ? (
            <div className="flex flex-col w-[95%] h-[80%] space-y-2 mt-2">
              {funcionarios?.map((funcionario) => (
                <div
                  key={funcionario.id}
                  className="flex flex-col w-full bg-slate-600 items-center justify-center"
                >
                  <span className="flex h-full w-full text-gray-100 text-start items-center justify-center text-md">
                    id: {funcionario.id}
                  </span>
                  <span className="flex h-full w-full text-gray-100 text-start items-center justify-center text-md">
                    Nome: {funcionario.nome}
                  </span>
                  <span className="flex h-full w-full text-gray-100 text-start items-center justify-center text-md">
                    CPF: {funcionario.cpf}
                  </span>
                </div>
              ))}
            </div>
          ) : !isLoading && funcionarios.length == 0 ? (
            <div className="bg-slate-600 mt-2">
              <span className="flex h-full w-full text-gray-100 text-center items-center justify-center text-xl">
                API indisponível, ou base sem dados
              </span>
            </div>
          ) : (
            <div>
              <Loading type="bars" color="#00BFFF"></Loading>
            </div>
          )}
        </div>
      </div>
      <div
        className={`flex flex-col w-[22%] h-[80%] bg-slate-700 rounded-lg border-8 ${
          produtos.length > 0 ? "border-green-600" : "border-red-600"
        } justify-start items-center`}
      >
        <div className="flex flex-row w-full h-[15%] items-center justify-center">
          <span className="flex h-full w-[70%] text-gray-100 text-center items-center justify-center text-2xl">
            Produtos
          </span>
          <button
            className="flex h-[70%] w-[20%] text-gray-100 text-center items-center border-2 justify-center rounded-full"
            onClick={() => fetchProdutos()}
          >
            <FaSync className="text-4xl" />
          </button>
        </div>
        <div className="flex flex-col w-[80%] h-[80%] overflow-y-scroll bg-gray-300 rounded-lg items-center">
          {!isLoading && produtos.length > 0 ? (
            <div className="flex flex-col w-[95%] h-[80%] space-y-2 mt-2">
              {produtos?.map((produto) => (
                <div
                  key={produto.id}
                  className="flex flex-col w-full bg-slate-600 items-center justify-center"
                >
                  <span className="flex h-full w-full text-gray-100 text-start items-center justify-center text-md">
                    id: {produto.id}
                  </span>
                  <span className="flex h-full w-full text-gray-100 text-start items-center justify-center text-md">
                    Nome: {produto.nome}
                  </span>
                  <span className="flex h-full w-full text-gray-100 text-start items-center justify-center text-md">
                    Quantidade: {produto.quantidade}
                  </span>
                  <span className="flex h-full w-full text-gray-100 text-start items-center justify-center text-md">
                    Preço: {produto.preco}
                  </span>
                </div>
              ))}
            </div>
          ) : !isLoading && produtos.length == 0 ? (
            <div className="bg-slate-600 mt-2">
              <span className="flex h-full w-full text-gray-100 text-center items-center justify-center text-xl">
                API indisponível, ou base sem dados
              </span>
            </div>
          ) : (
            <div>
              <Loading type="bars" color="#00BFFF"></Loading>
            </div>
          )}
        </div>
      </div>
      <div
        className={`flex flex-col w-[22%] h-[80%] bg-slate-700 rounded-lg border-8 ${
          vendas.length > 0 ? "border-green-600" : "border-red-600"
        } justify-start items-center`}
      >
        <div className="flex flex-row w-full h-[15%] items-center justify-center">
          <span className="flex h-full w-[70%] text-gray-100 text-center items-center justify-center text-2xl">
            Vendas
          </span>
          <button
            className="flex h-[70%] w-[20%] text-gray-100 text-center items-center border-2 justify-center rounded-full"
            onClick={() => fetchVendas()}
          >
            <FaSync className="text-4xl" />
          </button>
        </div>
        <div className="flex flex-col w-[80%] h-[80%] overflow-y-scroll bg-gray-300 rounded-lg items-center">
          {!isLoading && vendas.length > 0 ? (
            <div className="flex flex-col w-[95%] h-[80%] space-y-2 mt-2">
              {vendas?.map((venda) => (
                <div
                  key={venda.id}
                  className="flex flex-col w-full bg-slate-600 items-center justify-center"
                >
                  <span className="flex h-full w-full text-gray-100 text-start items-center justify-center text-md">
                    id: {venda.id}
                  </span>
                  <span className="flex h-full w-full text-gray-100 text-start items-center justify-center text-md">
                    id Cliente: {venda.idCliente}
                  </span>
                  <span className="flex h-full w-full text-gray-100 text-start items-center justify-center text-md">
                    id Funcionário: {venda.idFuncionario}
                  </span>
                  <span className="flex h-full w-full text-gray-100 text-start items-center justify-center text-md">
                    Nome: {venda.nome}
                  </span>
                </div>
              ))}
            </div>
          ) : !isLoading && vendas.length == 0 ? (
            <div className="bg-slate-600 mt-2">
              <span className="flex h-full w-full text-gray-100 text-center items-center justify-center text-xl">
                API indisponível, ou base sem dados
              </span>
            </div>
          ) : (
            <div>
              <Loading type="bars" color="#00BFFF"></Loading>
            </div>
          )}
        </div>
      </div>
    </div>
  );
}

export default App;
