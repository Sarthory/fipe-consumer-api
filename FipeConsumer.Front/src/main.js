import { createApp } from 'vue';
import App from './App.vue';
import router from '@/router/index';
import 'vuetify/styles';
import { createVuetify } from 'vuetify';
import * as components from 'vuetify/components';
import * as directives from 'vuetify/directives';
import '@mdi/font/css/materialdesignicons.css';
import pinia from './store';

const vuetify = createVuetify({
  components,
  directives,
  theme: {
    defaultTheme: 'light',
  },
});

const app = createApp(App);

app.use(vuetify);
app.use(router);
app.use(pinia);

app.mount('#app');
