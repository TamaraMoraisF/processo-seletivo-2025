import { useState, useEffect } from "react";
import axios from "axios";

export function useDuvCrud() {
    const [duvs, setDuvs] = useState([]);
    const [selectedDuv, setSelectedDuv] = useState(null);
    const [modalAberto, setModalAberto] = useState(false);
    const [duvEmEdicao, setDuvEmEdicao] = useState(null);

    useEffect(() => {
        carregarDuvs();
    }, []);

    const carregarDuvs = async () => {
        try {
            const response = await axios.get("https://localhost:7204/api/Duv");
            setDuvs(response.data);
            setSelectedDuv(response.data[0] || null);
        } catch (error) {
            console.error("Erro ao carregar DUVs:", error);
        }
    };

    const abrirFormularioDuv = (duv = null) => {
        setDuvEmEdicao(
            duv ?? {
                numero: "",
                dataViagem: new Date().toISOString().split("T")[0],
                navioId: 0,
                passageiros: [],
                nomePassageiro: "",
                nacionalidade: "",
                fotoUrl: "",
                sid: "",
                tipoPassageiro: 1
            }
        );
        setModalAberto(true);
    };

    const salvarDuv = async () => {
        try {
            let response;

            if (duvEmEdicao.id) {
                await axios.put(`https://localhost:7204/api/Duv/${duvEmEdicao.id}`, duvEmEdicao);
                response = { data: { id: duvEmEdicao.id } };
            } else {
                response = await axios.post("https://localhost:7204/api/Duv", duvEmEdicao);
            }

            const duvId = response.data.id;

            if (duvEmEdicao.pessoas && duvEmEdicao.pessoas.length > 0) {
                for (const pessoa of duvEmEdicao.pessoas) {
                    if (pessoa.tipo === 2 && !pessoa.sid?.trim()) {
                        alert(`SID é obrigatório para o tripulante ${pessoa.nome}`);
                        continue;
                    }

                    await axios.post("https://localhost:7204/api/Passageiro", {
                        nome: pessoa.nome,
                        nacionalidade: pessoa.nacionalidade,
                        fotoUrl: pessoa.fotoUrl,
                        sid: pessoa.tipo === 2 ? pessoa.sid : null,
                        tipo: pessoa.tipo,
                        duvId: duvId
                    });
                }
            }

            setModalAberto(false);
            await carregarDuvs();
        } catch (error) {
            console.error("Erro ao salvar DUV:", error);
        }
    };
    const removerDuv = async (id) => {
        try {
            await axios.delete(`https://localhost:7204/api/Duv/${id}`);
            await carregarDuvs();
        } catch (error) {
            console.error("Erro ao remover DUV:", error);
        }
    };

    return {
        duvs,
        selectedDuv,
        setSelectedDuv,
        modalAberto,
        duvEmEdicao,
        setDuvEmEdicao,
        abrirFormularioDuv,
        salvarDuv,
        removerDuv,
        setModalAberto
    };
}
