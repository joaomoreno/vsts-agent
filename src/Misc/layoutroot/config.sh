user_id=`id -u`

# we want to snapshot the environment of the config user
source ./env.sh

if [[ "$1" == "remove" ]]; then
    ./bin/Agent.Listener $*
else
    # user_name=`id -nu $user_id`

    ./bin/Agent.Listener configure $*
fi
