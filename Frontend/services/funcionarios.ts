import axios from "axios";

export const getFuncionarios = async () => {
  try {
    const response = await axios.get("http://localhost:5004/api/funcionarios", {
      headers: {
        "Content-Type": "application/json",
        "Access-Control-Allow-Origin": "http://localhost:5004/",
      },
    });
    console.log(response.data);
    return response.data;
  } catch (error: any) {
    throw new Error("Erro ao obter dados de Funcionarios: " + error.message);
  }
};
