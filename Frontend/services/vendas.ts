import axios from "axios";

export const getVendas = async () => {
  try {
    const response = await axios.get("http://localhost:5006/api/vendas", {
      headers: {
        "Content-Type": "application/json",
        "Access-Control-Allow-Origin": "http://localhost:5006/",
      },
    });
    console.log(response.data);
    return response.data;
  } catch (error: any) {
    throw new Error("Erro ao obter dados de Vendas: " + error.message);
  }
};
