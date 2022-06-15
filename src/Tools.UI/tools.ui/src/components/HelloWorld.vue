<script setup lang="ts">
import { ref } from 'vue'
import { SM4 } from 'gm-crypto';
defineProps<{ msg: string }>()

const count = ref(0)

// Convert a ASCII string to a hex string
function stringToHex(str:string):string{
    return str.split("").map(function(c) {
        return ("0" + c.charCodeAt(0).toString(16)).slice(-2);
    }).join("");
}

// const key = '0123456789abcdeffedcba9876543210' // Any string of 32 hexadecimal digits,任意32位的16进制数
// const key='735739335a4538726a4465443321316d'
var key: string = stringToHex('sW93ZE8rjDeD3!1m').toString();

const originalData = 'tuserid002'

/**
 * Block cipher modes:
 * - ECB: electronic codebook
 * - CBC: cipher block chaining
 */

let encryptedData, decryptedData

// ECB
encryptedData = SM4.encrypt(originalData, key, {
  inputEncoding: 'utf8',
  outputEncoding: 'base64'
})
console.log(encryptedData)
decryptedData = SM4.decrypt(encryptedData, key, {
  inputEncoding: 'base64',
  outputEncoding: 'utf8'
})

// CBC
const iv = '0123456789abcdeffedcba9876543210' // Initialization vector(any string of 32 hexadecimal digits)
encryptedData = SM4.encrypt(originalData, key, {
  iv,
  mode: SM4.constants.CBC,
  inputEncoding: 'utf8',
  outputEncoding: 'hex'
})
decryptedData = SM4.decrypt(encryptedData, key, {
  iv,
  mode: SM4.constants.CBC,
  inputEncoding: 'hex',
  outputEncoding: 'utf8'
})

</script>

<template>
  <h1>{{ msg }}</h1>

  <p>
    Recommended IDE setup:
    <a href="https://code.visualstudio.com/" target="_blank">VS Code</a>
    +
    <a href="https://github.com/johnsoncodehk/volar" target="_blank">Volar</a>
  </p>

  <p>See <code>README.md</code> for more information.</p>

  <p>
    <a href="https://vitejs.dev/guide/features.html" target="_blank">
      Vite Docs
    </a>
    |
    <a href="https://v3.vuejs.org/" target="_blank">Vue 3 Docs</a>
  </p>

  <button type="button" @click="count++">count is: {{ count }}</button>
  <p>
    Edit
    <code>components/HelloWorld.vue</code> to test hot module replacement.
  </p>
</template>

<style scoped>
a {
  color: #42b983;
}

label {
  margin: 0 0.5em;
  font-weight: bold;
}

code {
  background-color: #eee;
  padding: 2px 4px;
  border-radius: 4px;
  color: #304455;
}
</style>
