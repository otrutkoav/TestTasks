Vue.createApp({
    data() {
      return {
          pathApi:"https://localhost:5010/api/Task",
          list:[],
          task:{},
          addEditModalShow:false,
          modalTitle:'Добавить задачу',
          statusList:[{Key:0,Value:'To Do'},{Key:1,Value:'In progress'},{Key:2,Value:'Done'},{Key:3,Value:'Removed'}]
      }
    },
    mounted(){
        this.GetTasks();
    },
    methods: {
      remove(key){
        let _this=this;
        axios.delete(_this.pathApi+'/' + key)
        .then((response) => {
          console.log(response);
          _this.GetTasks();
        })
        .catch((error) => {
          console.log(error);
        });
      },
      createTask(data){
        let _this=this;
        _this.task={
          Key:data.id,
          Name:data.name,
          Description:data.description,
          Status:_this.statusList.filter(function (status) {
            if (status.Key == data.status) {
                return status;
            }
        })[0]
        };
      },
      chanel(){
        this.close();
      },
      close(){
        this.addEditModalShow=false;
      },   
      async GetTasks() {
          let _this=this;

          axios
          .get(_this.pathApi)
          .then(response => (

            _this.list = response.data.map(function (item)
             {
                return {
                  Key:item.id,
                  Name:item.name
                }
             })
            ));
      },
      edit(type,key)
      {
        let _this=this;

        _this.addEditModalShow=true;

        if(type==="add"){
          _this.modalTitle='Добавить задачу';
            _this.task={
              Name:'',
              Status:0
            };
        }
        if(type==="edit"){
          _this.modalTitle="Редактировать задачу"
          _this.getTask(key);
        }
      },
      getTask(key){

        let _this=this;
        
        axios
        .get(_this.pathApi+'/' + key)
        .then(function (response) {
         _this.createTask(response.data);
        })
        .catch(function (error) {
          console.log(error);
        });

      },

      save(){
        let _this=this;
        
        let data={
          Id:_this.task.Key,
          Name:_this.task.Name,
          Description:_this.task.Description,
          Status:_this.task.Status.Key
        };
        axios.post(_this.pathApi, data)
        .then((response) => {
          console.log(response);
          _this.GetTasks();
          _this.close();
        })
        .catch((error) => {
          console.log(error);
        });
      },   
    }
  }).mount('#app');
  