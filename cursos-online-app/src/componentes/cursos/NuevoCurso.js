import React from 'react';
import { Button, Container, Grid, TextField, Typography } from '@material-ui/core';
import style from '../Tool/Style';
import { KeyboardDatePicker, MuiPickersUtilsProvider } from '@material-ui/pickers';
import DateFnsUtils from '@date-io/date-fns';
import { useState } from 'react';


const NuevoCurso = () => {
    const [fechaSeleccionada, setFechaSeleccionada] = useState(new Date());

    const [curso, setCurso] = useState({
        titulo: '',
        descripcion: '',
        precio: 0.0,
        promocion: 0.0
    });

    const ingresarValoresMemoria = e => {
        const { name, value } = e.target;
        setCurso( (anterior) => ({
            ...anterior,
            [name]: value
        }));
    }

    return (
      <Container component="main" maxWidth="md" justity="center">
        <div style={style.paper}>
            <Typography component="h1" variant="h5">
                Registro de nuevo curso
            </Typography>

            <form style={style.form}>
                <Grid container spacing={2}>
                    <Grid item xs={12} md={12}>
                        <TextField
                        name="titulo"
                        variant="outlined"
                        fullWidth
                        label="Ingrese Titulo"
                        value = {curso.titulo}
                        onChange= {ingresarValoresMemoria}
                        />
                    </Grid>

                    <Grid item xs={12} md={12}>
                        <TextField
                        name="descripcion"
                        variant="outlined"
                        fullWidth
                        label="Ingrese descripcion"
                        value = {curso.descripcion}
                        onChange= {ingresarValoresMemoria}
                        />
                    </Grid>

                    <Grid item xs={12} md={6}>
                        <TextField
                        name="precio"
                        variant="outlined"
                        fullWidth
                        label="Ingrese Precio Normal"
                        value = {curso.precio}
                        onChange= {ingresarValoresMemoria}
                        />
                    </Grid>

                    <Grid item xs={12} md={6}>
                        <TextField
                        name="promocion"
                        variant="outlined"
                        fullWidth
                        label="Ingrese Precio Promocion"
                        value = {curso.promocion}
                        onChange= {ingresarValoresMemoria}
                        />
                    </Grid>

                    <Grid item xs={12} md={6}>
                       <MuiPickersUtilsProvider utils={DateFnsUtils}>
                        <KeyboardDatePicker 
                        value={fechaSeleccionada}
                        onChange={setFechaSeleccionada}
                        margin="normal"
                        id="fecha-publicacion-id"
                        label="Seleccione fecha de publicacion"
                        format="dd/MM/yyyy"
                        fullWidth
                        KeyboardButtonProps = {{
                            "aria-label": "change date"
                        }}
                        />
                       </MuiPickersUtilsProvider>
                    </Grid>
                </Grid>
            
                <Grid container justify="center">
                    <Grid item xs={12} md={6}>
                        <Button 
                        type="submit"
                        fullWidth
                        variant="contained"
                        color="primary"
                        style={style.submit}
                        >
                        Guardar Curso
                        </Button>
                       
                    </Grid>
                </Grid>
            </form>
        </div>
      </Container>
    );
};

export default NuevoCurso;