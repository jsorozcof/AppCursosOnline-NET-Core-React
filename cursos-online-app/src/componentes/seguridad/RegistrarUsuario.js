import React, { useState } from "react";
import { Button, Container, Grid, TextField, Typography } from "@material-ui/core";
import style from '../Tool/Style';
import { registrarUsuario } from '../../actions/UsuarioAction';

const RegistrarUsuario = () => {

  const [usuario, setUsuario] = useState({
    NombreCompleto : '',
    Email: '',
    Password: '',
    ConfirmarPassword: '',
    UserName: ''
  });

  const ingresarValoresMemoria = e => {
      const {name, value} = e.target;
      setUsuario(anterior => ({
        ...anterior,
        [name]: value
        //NombreCompleto: 'faber orozco'
      }))
  }

  const registrarUsuarioClick = e => {
    e.preventDefault();
    
    registrarUsuario(usuario).then(response => {
      console.log('Se registro exitosamente el usuario', response);
      window.localStorage.setItem("token_seguridad", response.data.token);
    });
  }


  return (
    <Container component="main" maxWidth="md" justify="center">
      <div style={style.paper}>
        <Typography component="h1" variant="h5">
          Registro de Usuario
        </Typography>
        <form style={style.form}>
          <Grid container spacing={2}>
            <Grid item xs={12} md={12}>
              <TextField
                name="NombreCompleto" value={usuario.NombreCompleto} onChange={ingresarValoresMemoria}
                variant="outlined"
                fullWidth
                label="Ingrese su nombre y apellido"
              />
            </Grid>

            <Grid item xs={12} md={6}>
              <TextField
                name="Email" value={usuario.Email} onChange={ingresarValoresMemoria}
                variant="outlined"
                fullWidth
                label="Ingrese su email "
              />
            </Grid>

            <Grid item xs={12} md={6}>
              <TextField value={usuario.UserName} onChange={ingresarValoresMemoria}
                name="UserName"
                variant="outlined"
                fullWidth
                label="Ingrese su username "
              />
            </Grid>

            <Grid item xs={12} md={6}>
              <TextField value={usuario.Password} onChange={ingresarValoresMemoria}
                name="Password"
                type="password"
                variant="outlined"
                fullWidth
                label="Ingrese password"
              />
            </Grid>

            <Grid item xs={12} md={6}>
              <TextField value={usuario.ConfirmarPassword} onChange={ingresarValoresMemoria}
                name="ConfirmarPassword"
                type="password"
                variant="outlined"
                fullWidth
                label="Confirme password"
              />
            </Grid>

            <Grid container  justify="center">
                <Grid item xs={12} md={6}>
                    <Button type="submit" onClick={registrarUsuarioClick} fullWidth variant="contained" color="primary" size="large" style={style.submit}>
                        Enviar
                    </Button>
                </Grid>
            </Grid>

          </Grid>
        </form>
      </div>
    </Container>
  );
};

export default RegistrarUsuario;
