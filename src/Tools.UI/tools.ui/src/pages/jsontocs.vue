<template>
  <n-card title="JSON转C#" class="h-full shadow-sm rounded-16px">
    <div class="flex items-center gap-2 mb-2">
      <n-button @click="generateCSharpClass" type="primary"
        >JSON转C#类</n-button
      >
      <n-button type="primary" ghost @click="copy(result)"> 复制结果 </n-button>
    </div>
    <n-grid x-gap="24" :cols="2">
      <n-gi>
        <monacoEditor
          theme="vs"
          v-model="code"
          :language="language"
          width="800px"
          height="700px"
          @change="handleEditorChange"
          ref="codeRef"
        ></monacoEditor>
      </n-gi>
      <n-gi>
        <monacoEditor
          theme="vs"
          v-model="result"
          :language="languageCs"
          width="800px"
          height="700px"
        ></monacoEditor
      ></n-gi>
    </n-grid>
  </n-card>
</template>

<script lang="ts" setup>
import monacoEditor from "@/components/monacoEditor/index.vue";
import { ref } from "vue";
import API from "@/api";
const code = ref("");
const language = ref("json");
const codeRef = ref<any>(null);

const result = ref("");
const languageCs = ref("csharp");

// 处理编辑器内容变化的方法
const handleEditorChange = (value: string) => {
  console.log("Editor content changed in parent component:", value);
  if (codeRef.value != null) {
    codeRef.value.formatCode();
  }
};
const message = useMessage();
const generateCSharpClass = async () => {
  if (code.value == null || code.value == "") {
    message.error("请输入JSON内容");
    return;
  }
  result.value = await API.GenerateCSharpClass({
    json: JSON.stringify(code.value),
    className: "RootObject",
  });
};
const copy = (text: string) => {
  navigator.clipboard.writeText(text);
  message.success("复制成功");
};
</script>
