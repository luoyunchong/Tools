 
<template>
  <n-card title="MarkDown预览" class="h-full shadow-sm rounded-16px">
    <v-md-editor :text="markdownTxt" height="600px"></v-md-editor>
  </n-card>
</template>

<script lang="ts" setup>
import VMdEditor from '../components/VMdEditor';
const markdownTxt = ref('')
</script>
