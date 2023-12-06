import axios from "axios";

export const getProdutos = async () => {
  try {
    const response = await axios.get("http://localhost:5000/api/produtos", {
      headers: {
        "Content-Type": "application/json",
        "Access-Control-Allow-Origin": "http://localhost:5000/",
      },
    });
    console.log(response.data);
    return response.data;
  } catch (error: any) {
    throw new Error("Erro ao obter dados de Vendas: " + error.message);
  }
};
