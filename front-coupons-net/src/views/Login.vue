<template>
    <v-container fluid class="container-principal">
        <v-row justify="center">
            <v-col cols="12" sm="8" md="4">
                <v-card>
                    <v-toolbar color="#324c6e" dark flat>
                        <v-img style="left: 25px;" max-height="50" max-width="130" src="/neocode.png"
                            alt="Vuetify"></v-img>
                    </v-toolbar>
                    <v-card-text>
                        <v-form>
                            <v-text-field label="Email" name="email" prepend-icon="mdi-account" type="text"
                                v-model="loginStore.user.email"></v-text-field>

                            <v-text-field label="Password" name="password" prepend-icon="mdi-lock" type="password"
                                v-model="loginStore.user.password"></v-text-field>
                            
                            
                            <div v-if="loginStore.errorLoginMessages.length > 0" style="color: red; text-align: center">
                            <ul>
                                <li v-for="error in loginStore.errorLoginMessages" :key="error">
                                {{ error }}
                                </li>
                            </ul>
                            </div>
                        </v-form>
                        
                        <v-btn
                            class="font-weight-bold" 
                            color="#324c6e" 
                            text
                            to="/forgotPasword"
                        >
                        Olvidé mi contraseña
                        </v-btn>


                    </v-card-text>
                    <v-card-actions>
                        <v-spacer></v-spacer>
                        <v-btn class="font-weight-bold" color="#324c6e" text @click="onLogin()">
                            <v-icon>mdi-arrow-right-bold mdi-24px</v-icon>Ingresar
                        </v-btn>
                    </v-card-actions>
                </v-card>
            </v-col>
        </v-row>
    </v-container>
</template>

<script setup>
import { useRouter } from 'vue-router'
import { useLoginStore } from '../stores/loginStore';

const loginStore = useLoginStore();
const router = useRouter();

const validarEmail = (email) => {
  const re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
  return re.test(String(email).toLowerCase());
};

const validateCredentials = () => {
    loginStore.cleanLogin()
    if (!loginStore.user?.email) loginStore.errorLoginMessages.push("Ingrese email");
    if (!loginStore.user?.password) loginStore.errorLoginMessages.push("Ingrese password");
    if (!validarEmail(loginStore.user.email)) loginStore.errorLoginMessages.push("Ingrese un correo electrónico válido");
    if (loginStore.errorLoginMessages.length > 0) loginStore.errorLogin = true;
    return loginStore.errorLogin;
};

const onLogin = async () => {
    if (validateCredentials()) {
        return
    }

   loginStore.login(loginStore.user).then((response) => {
        if (response.status === 200) {
            router.push({
                name: 'home'
            })
        }else{
            loginStore.errorLoginMessages.push(response.response.data);
        }
    });
}
</script>

<style>
.container-principal {
    min-height: 100vh;
    background: #eceff1;
    overflow: hidden;
    display: flex;
    align-items: center;
    justify-content: center;
}
</style>