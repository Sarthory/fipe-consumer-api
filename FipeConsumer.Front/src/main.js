import { createApp } from "vue";
import App from "./App.vue";
import router from "@/router/index";

const app = createApp(App);

app.use(router);
// app.use(pinia) // Caso utilize Pinia

app.mount("#app");
