import React, { useState, useEffect } from 'react';


export default function ControlTyping(texto, delay) {
    const [textoValor, setTextoValor] = useState();

    useEffect(() => {
        const manejador = setTimeout(() => {
            setTextoValor(texto);
        }, delay);

        return () => {
            clearTimeout(manejador)
        }
    }, [texto]);

    return textoValor;
}