import { useState } from "react";
import reactLogo from "./assets/react.svg";
import viteLogo from "/vite.svg";
import { FaSync } from "react-icons/fa";
import "./App.css";

function App() {
  const [count, setCount] = useState(0);

  return (
    <div className="flex flex-row bg-gray-400 w-screen h-screen p-4 justify-evenly items-center">
      <div className="flex flex-col w-[22%] h-[80%] bg-slate-700 rounded-lg border-8 border-green-600 justify-start items-center">
        <div className="flex flex-row w-full h-[15%] items-center justify-center">
          <span className="flex h-full w-[70%] text-gray-100 text-center items-center justify-center text-2xl">
            Clientes
          </span>
          <button className="flex h-[70%] w-[20%] text-gray-100 text-center items-center border-2 justify-center rounded-full">
            <FaSync className="text-4xl" />
          </button>
        </div>

        <div className="flex flex-col w-[80%] h-[80%] bg-gray-300 rounded-lg"></div>
      </div>
      <div className="flex flex-col w-[22%] h-[80%] bg-slate-700 rounded-lg border-8 border-green-600 justify-start items-center">
        <div className="flex flex-row w-full h-[15%] items-center justify-center">
          <span className="flex h-full w-[70%] text-gray-100 text-center items-center justify-center text-2xl">
            Funcionários
          </span>
          <button className="flex h-[70%] w-[20%] text-gray-100 text-center items-center border-2 justify-center rounded-full">
            <FaSync className="text-4xl" />
          </button>
        </div>
        <div className="flex flex-col w-[80%] h-[80%] bg-gray-300 rounded-lg"></div>
      </div>
      <div className="flex flex-col w-[22%] h-[80%] bg-slate-700 rounded-lg border-8 border-green-600 justify-start items-center">
        <div className="flex flex-row w-full h-[15%] items-center justify-center">
          <span className="flex h-full w-[70%] text-gray-100 text-center items-center justify-center text-2xl">
            Produtos
          </span>
          <button className="flex h-[70%] w-[20%] text-gray-100 text-center items-center border-2 justify-center rounded-full">
            <FaSync className="text-4xl" />
          </button>
        </div>
        <div className="flex flex-col w-[80%] h-[80%] bg-gray-300 rounded-lg"></div>
      </div>
      <div className="flex flex-col w-[22%] h-[80%] bg-slate-700 rounded-lg border-8 border-green-600 justify-start items-center">
        <div className="flex flex-row w-full h-[15%] items-center justify-center">
          <span className="flex h-full w-[70%] text-gray-100 text-center items-center justify-center text-2xl">
            Vendas
          </span>
          <button className="flex h-[70%] w-[20%] text-gray-100 text-center items-center border-2 justify-center rounded-full">
            <FaSync className="text-4xl" />
          </button>
        </div>
        <div className="flex flex-col w-[80%] h-[80%] bg-gray-300 rounded-lg"></div>
      </div>
    </div>
  );
}

export default App;
